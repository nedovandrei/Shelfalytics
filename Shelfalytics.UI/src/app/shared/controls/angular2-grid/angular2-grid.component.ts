import {Component, ViewEncapsulation} from '@angular/core';
import {NgGridModule, NgGridItem, NgGridConfig, NgGridItemConfig, NgGridItemEvent} from 'angular2-grid';

interface Box {
    id: number;
    config: any;
}

@Component({
    selector: 'angular2-grid',
    templateUrl: 'angular2-grid.html',
    styleUrls: ['angular2-grid.css'],
	encapsulation: ViewEncapsulation.None
})
export class AngularGridComponent {
	private boxes: Array<Box> = [];
	private rgb: string = 'transparent';
	private curNum;
	private gridConfig: NgGridConfig = <NgGridConfig>{
		'margins': [5],
		'draggable': true,
		'resizable': true,
		'max_cols': 4,
		'max_rows': 0,
		'visible_cols': 0,
		'visible_rows': 0,
		'min_cols': 0,
		'min_rows': 0,
		'col_width': 1,
		'row_height': 1,
		'cascade': 'left',
		'min_width': 20,
		'min_height': 20,
		'fix_to_grid': true,
		'auto_style': true,
		'auto_resize': true,
		'maintain_ratio': false,
		'prefer_new': false,
		'zoom_on_drag': false,
		'limit_to_screen': false
	};
	private itemPositions: Array<any> = [];

	constructor() {
		const dashconf = this._generateDefaultDashConfig();
		for (var i = 0; i < dashconf.length; i++) {
			const conf = dashconf[i];
			conf.payload = 1 + i;
			this.boxes[i] = { id: i + 1, config: conf };
		}
		this.curNum = dashconf.length + 1;
	}

	addBox(): void {
		const conf: NgGridItemConfig = this._generateDefaultItemConfig();
		conf.payload = this.curNum++;
		this.boxes.push({ id: conf.payload, config: conf });
	}

	removeWidget(index: number): void {
		if (this.boxes[index]) {
			this.boxes.splice(index, 1);
		}
	}

	updateItem(index: number, event: NgGridItemEvent): void {
		// Do something here
	}

	onDrag(index: number, event: NgGridItemEvent): void {
		// Do something here
	}

	onResize(index: number, event: NgGridItemEvent): void {
		// Do something here
	}

	private _generateDefaultItemConfig(): NgGridItemConfig {
		return { 'dragHandle': '.handle', 'col': 1, 'row': 1, 'sizex': 1, 'sizey': 1 };
	}

	private _generateDefaultDashConfig(): NgGridItemConfig[] {
		return [{ 'dragHandle': '.handle', 'col': 1, 'row': 1, 'sizex': 1, 'sizey': 1 },
		{ 'dragHandle': '.handle', 'col': 1, 'row': 1, 'sizex': 1, 'sizey': 1 }
		// { 'dragHandle': '.handle', 'col': 26, 'row': 1, 'sizex': 1, 'sizey': 1 },
		// { 'dragHandle': '.handle', 'col': 51, 'row': 1, 'sizex': 75, 'sizey': 1 },
		// { 'dragHandle': '.handle', 'col': 51, 'row': 26, 'sizex': 32, 'sizey': 40 },
		// { 'dragHandle': '.handle', 'col': 83, 'row': 26, 'sizex': 1, 'sizey': 1 }
  ];
	}
}
