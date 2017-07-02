import {Component, Input, OnInit} from '@angular/core';

import {PieChartService} from './pieChart.service';

import 'easy-pie-chart/dist/jquery.easypiechart.js';

@Component({
  selector: 'pie-chart',
  templateUrl: './group-pieChart.html',
  styleUrls: ['./pieChart.scss'],
  providers: [PieChartService]
})
// TODO: move easypiechart to component
export class GroupPieChart implements OnInit {

  @Input() chartData: any;
  @Input() rowCount: number;

  public charts: Array<Object>;
  private _init = false;

  constructor(private _pieChartService: PieChartService) { }
  ngOnInit() {
    this.charts = this._pieChartService.getData(this.chartData, this.rowCount);
    this._init = true;
  }

  ngAfterViewInit() {
    this._loadPieCharts();
    // this._updatePieCharts();
  }

  private _loadPieCharts() {

    jQuery('.group-chart').each(function () {
      let chart = jQuery(this);
      chart.easyPieChart({
        easing: 'easeOutBounce',
        onStep: function (from, to, percent) {
          jQuery(this.el).find('.percent').text(Math.round(percent));
        },
        barColor: jQuery(this).attr('data-rel'),
        trackColor: 'rgba(0,0,0,0)',
        size: 84,
        scaleLength: 0,
        animation: 2000,
        lineWidth: 9,
        lineCap: 'round',
      });
    });
  }

  private _updatePieCharts() {
    jQuery('.pie-charts .group-chart').each(function(index, chart) {
      jQuery(chart).data('easyPieChart').update(parseInt(jQuery(chart).attr("data-percent")));
    });
  }
}
