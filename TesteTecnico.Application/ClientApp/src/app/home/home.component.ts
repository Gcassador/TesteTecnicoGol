import { Component, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public airplanes: Airplane[];
  AirPlaneForm;
  http: HttpClient;
  baseUrl: string;

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private formBuilder: FormBuilder) {
    this.http = http;
    this.baseUrl = baseUrl;

    this.airplanes = [];
    this.getAirPlanes();

    this.AirPlaneForm = this.formBuilder.group({
      id: null,
      model: '',
      numberOfPassenger: ''
    })
  }
  getAirPlanes() {
    this.http.get<Airplane[]>(this.baseUrl + 'Airplane').subscribe(result => {
      this.airplanes = result;
    }, error => console.error(error));
  }

  onSubmit(airPlane) {
    var newAirPlane: Airplane = {
      id: airPlane.id || 0,
      model: airPlane.model,
      numberOfPassenger: airPlane.numberOfPassenger,
      createdDate: new Date()
    };
    if (airPlane.id) {
      this.http.put(this.baseUrl + 'Airplane', newAirPlane).pipe(

        catchError((err, result) => {
          console.log(err);

          return of(result);
        })
      ).subscribe(() => {
        this.airplanes[this.airplanes.findIndex(x => x.id == newAirPlane.id)] = newAirPlane;
      });
    }
    else {
      this.http.post<number>(this.baseUrl + 'Airplane', newAirPlane)
        .pipe(

          catchError((err, result) => {
            console.log(err);

            return of(result);
          })
        ).subscribe((data: number) => {
          newAirPlane.id = data;
          this.airplanes.push(newAirPlane);
        });
    }
    this.AirPlaneForm = this.formBuilder.group({
      id: null,
      model: '',
      numberOfPassenger: ''
    })
  }
  Edit(airplane: Airplane) {
    this.AirPlaneForm = this.formBuilder.group({
      id: airplane.id,
      model: airplane.model,
      numberOfPassenger: airplane.numberOfPassenger
    })
  }

  Delete(id: number) {
    this.http.delete(this.baseUrl + 'Airplane?Id=' + id )
      .pipe(

        catchError((err, result) => {
          console.log(err);

          return of(result);
        })
    ).subscribe((data: number) => {
      this.airplanes.splice(this.airplanes.findIndex(x => x.id == id), 1);
    });
  }
}

interface Airplane {
  id?: number;
  model: string;
  numberOfPassenger: number;
  createdDate: Date;
}
