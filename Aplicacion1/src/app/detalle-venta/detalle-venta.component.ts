import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-detalle-venta',
  templateUrl: './detalle-venta.component.html',
  styleUrls: ['./detalle-venta.component.css']
})
export class DetalleVentaComponent implements OnInit {
  dtOptions: DataTables.Settings = {};
  @Input() lstDetalles: any;
  @Input() codigo: number;
  constructor() { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10
    };
  }

}
