import { Component, OnInit, Input } from '@angular/core';
import * as $ from "jquery";

@Component({
  selector: 'easy-pie-chart',
  templateUrl: './easy-pie-chart.component.html',
  styleUrls: ['./easy-pie-chart.component.scss']
})
export class EasyPieChartComponent implements OnInit {

  constructor() { }

  @Input() chartData: any;
  private initFlag: boolean = false;

  ngOnInit() {
    console.log("easypiechart chartData", this.chartData);
    this.initFlag = true;
  }

  ngAfterViewInit() {
    this._loadPieCharts();
    // this._updatePieCharts();

  }

  private _loadPieCharts() {
    
    console.log("easypiechart chartData", this.chartData);
    jQuery('.easy-pie-chart').easyPieChart({
      easing: 'easeOutBounce',
      onStep: function (from, to, percent) {
        jQuery(this.el).find('.percent').text(Math.round(percent));
      },
      barColor: this.chartData.color,
      trackColor: 'rgba(0,0,0,0.4)',
      size: 250,
      scaleLength: 0,
      animation: 2000,
      lineWidth: 15,
      lineCap: 'round',
    });



  }

  private _updatePieCharts() {
    jQuery('.pie-charts .easy-pie-chart').each(function(index, chart) {
      jQuery(chart).data('easyPieChart').update(parseInt(jQuery(chart).attr("data-percent")));
    });
  }
}
