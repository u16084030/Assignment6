import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { fromEvent } from 'rxjs';
import { map, filter, distinctUntilChanged, switchMap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class ReportingService {

  constructor(private _http : HttpClient) { }

  getReportingData(selection)
  {
    return this._http.get("http://localhost:2875/api/Report/getReportData?flightSelection="+selection)
    .pipe(map(result => result));
  }

  downloadReport(selection, type)
  {
    return this._http.get("http://localhost:2875/api/Report/downloadReport?flightSelection="+selection+"&type="+type,{responseType : 'blob'});
  }
}
