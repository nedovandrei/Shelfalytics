export interface IGoogleMapsData {
    center: ICoordinates;
    zoom?: number;
    markers: IMarker[];
    disableDefaultUI?: boolean;
    zoomControl?: boolean;
    clickableIcons?: boolean;
    draggableCursor?: string; // [draggableCursor]="url(<some address>), pointer"
    height?: number; // in px. default = 400
    // scrollwheel: boolean;
}

export interface ICoordinates {
    lat: number;
    lng: number;
}

export interface IMarker {
    coordinates: ICoordinates;
    title: string;
    markerDraggable: boolean;
    iconUrl?: any;
    label?: string; // a single uppercase letter
    opacity?: number;
    visible?: boolean;
    infoWindowContent: string;
    linkUrl?: string;
    linkUrlId?: number;  
}
