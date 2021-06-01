import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators} from '@angular/forms';
import { ApiVentaService } from '../services/api-venta.service';
import Swal from 'sweetalert2'
import { Venta } from '../models/Venta';

@Component({
  selector: 'app-venta',
  templateUrl: './venta.component.html',
  styleUrls: ['./venta.component.css']
})
export class VentaComponent implements OnInit {

  crearVenta:boolean = false;
  submitted:boolean = false;
  venta: Venta = {} as Venta;
  lstVenta: any;
  lstClientes: any;
  fechaActual: any;
  lstProductos: any;
  dtOptions: DataTables.Settings = {};
  detallesVenta: any;
  mostrarDetalle: boolean = false;
  codigo_Venta: number;
  constructor(private formBuilder:FormBuilder, private apiVenta: ApiVentaService) { }

  formulario = this.formBuilder.group({
    CodigoVenta: ['',Validators.required],
    FechaVenta: ['',Validators.required],
    ClienteId: ['',Validators.required],
    ProductosIds: ['',Validators.required],
    ValorVenta: ['',Validators.required]
  })

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10
    };
    this.getVenta();
    this.getclientes();
    this.getProductos();
    this.fechaActual = new Date().toISOString();
  }

  get f(){return this.formulario.controls} //esta sentencia sirve para ponerle un alias al formulario.

  resetFormulario(){
    this.formulario.reset();
  }

  getclientes(){
    this.apiVenta.GetCliente().subscribe(response=>{
      if(response.exito == 1)
      {
        this.lstClientes = response.datos;
      }
    })
  }

  getVenta(){
    this.apiVenta.GetVenta().subscribe(response=>{
      if(response.exito == 1)
      {
        this.lstVenta = response.datos;
        console.log(this.lstVenta);
      }
    })
  }

  getProductos(){
    this.apiVenta.GetProducto().subscribe(response=>{
      if(response.exito == 1)
      {
        this.lstProductos = response.datos;
        console.log(response.datos);
      }
    })
  }

  AddVenta() {
    this.submitted = true;
    if (this.formulario.invalid) {
      return;
    }

    Swal.fire({
      title: 'Venta',
      text: '¿Desea guardar la venta?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Si, guardar',
      cancelButtonText: 'No, cancelar'
    }).then((result) => {
      if (result.value) 
      {
    
        this.venta = Object.assign(this.venta, this.formulario.value);
        console.log(this.venta);
        this.apiVenta.AddVenta(this.venta).subscribe(response => {
          if(response.exito == 0){
            console.log(response.mensaje);
            return;
          }
          Swal.fire({
            title: 'Venta',
            text: 'La venta se guardo con exito',
            icon: 'success',
            showCancelButton: false,
          })     
           
        })
    
      }
  })
  }

  detalleVenta(detalle, codigo){
    this.detallesVenta = detalle;
    this.codigo_Venta = codigo;
    this.mostrarDetalle = true;
    console.log(detalle);
  }

  sancionar(clienteId, ventaId){
    // this.apiVenta.metodoSancionar(clienteId, ventaId);
  }

}
