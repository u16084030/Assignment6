import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Chart } from 'chart.js';
import { ReportingService } from './reporting.service'
import { mergeMap, groupBy, map, reduce } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'AngularReport';

  chart = [];
  flights :Object=null;
  options = [
    {id: 1, text: "Local Flights"},
    {id: 2, text: "International Flights"},
    {id: 3, text: "All Flights"}
  ]
  selection:Number = 3;

  constructor(private reporting: ReportingService) {}

  random_rbga() {
    var o = Math.round, r = Math.random, s=255;
    return 'rgba(' + o(r()*s) + ',' + o(r()*s) + ',' + o(r()*s) + ',0.7)';
  }

  submitRequest(){
    this.reporting.getReportingData(this.selection).subscribe(response => {
      console.log(response);

      let keys = response["Airlines"].map(d=>d.Name);
      let values = response["Airlines"].map(d=>d.Popularity);

      this.flights = response["Flights"];

      this.chart = new Chart('canvas', {
        type: 'bar',
        data: {
          labels: keys,
          datasets: [
            {
              data: values,
              borderColor: "#3cba9f",
              fill: false,
              backgroundColor : [
                this.random_rbga(),
                this.random_rbga(),
                this.random_rbga(),
                this.random_rbga(),
                this.random_rbga(),
                this.random_rbga(),
                this.random_rbga(),
                this.random_rbga(),
                this.random_rbga(),
                this.random_rbga(),
                this.random_rbga(),
                this.random_rbga(),
              ]
            }
          ]
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
          legend: {
            display: false
          },
          title: {
            display: true,
            text: "Flights Booked Per Airline"
          },
          scales: {
            xAxes: [{
              display: true,
              barPercentage: 0.75
            }],
            yAxes: [{
              display: true,
              ticks: {
                min: 0,
                max: 50
              }
            }],
          }
        }
      })
    });
  }

  /*downloadRequest(type){
    this.reporting,downloadReport(this.selection, type).subscribe(x => {
      var fileType = type==1?"application/pdf": "application/msword";
      var fileName = type==1? "report.pdf" : "report.doc";
      var newBlob = new Blob([x], {type: fileType});

      if (window.navigator && window.navigator.msSaveOrOpenBlob) {
        window.navigator.msSaveOrOpenBlob(newBlob);
        return;
      }

      const data = window.URL.createObjectURL(newBlob);

      var link = document.createElement('a');
      link.href= data;
      link.download = fileName;

      link.dispatchEvent(new MouseEvent('click', {bubbles: true, cancelable: true, view: window}));

      setTimeout(function () {

      })
    })
  }*/


}
