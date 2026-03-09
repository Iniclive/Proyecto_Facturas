using System;
using System.Linq;
using System.Data.SqlClient;
using Proyecto_Facturas.Data;
using inercya.EntityLite.Extensions;
using inercya.EntityLite; // Ajusta al nombre de tu namespace

class Program
{
    static void Main(string[] args)
    {
        string connString = @"Data Source=(localdb)\InigoPracticaDB;Initial Catalog=PracticaFacturacionDB;Integrated Security=True";

        // Instanciamos el servicio
        var db = new FacturacionDataService(connString);

        Console.WriteLine("Conectando a la base de datos...");

       

             //Insert sencillo
             /*var nuevoEstado = new Master_data();
             nuevoEstado.NombreEstado = "Facturado";
             db.Master_dataRepository.Insert(nuevoEstado);

             Console.WriteLine($"¡Registro {nuevoEstado.NombreEstado} insertado correctamente!");

             //Metodo get, tiene una "projection" y un id para filtrar, la proyection puede crearse o estar ya definida desde una Vista
             /*Master_data md = db.Master_dataRepository.Get(Master_dataProjections.BaseTable, 1);
             if (md != null) { Console.WriteLine(md.ToString()); }*/

        //Delete sencillo
        /*if (db.Master_dataRepository.Delete(5)) {
            Console.WriteLine($"Se ha borrado el registro");
        }*/

        //Query con un select* para recuperar todos los datos
        /*var mdLista = db.Master_dataRepository.Query(Master_dataProjections.BaseTable).ToList();

        foreach(var md in mdLista)
        {
            Console.WriteLine(md.IdEstado + " : " + md.NombreEstado);
        }*/

        /*Insurance ins = new Insurance();
        ins.Name = "AseguradoraPrueba3";
        try
        {
            db.InsuranceRepository.Save(ins);
            Console.WriteLine(ins.Name + " insertado correctamente");
        }
        catch (Exception ex) {

        }*/

        /*User us = new User();
         us.Name = "Test3";
         us.Password = "test3";
         db.UserRepository.Insert(us);*/


        /*Factura fac = new Factura();
        fac.NumeroFactura = "6789";
        fac.FechaFactura = DateTime.Now;
        fac.Aseguradora = 2;
        fac.Creado = DateTime.Now;
        fac.CreadoPor = 2;
        fac.Status = 1;
        fac.Modificado = DateTime.Now;
        fac.ModificadoPor = 1;
        fac.Importe = (decimal?)30.58;
        fac.TipoIva = 5;
        try
        {
            db.FacturaRepository.Save(fac);
        }
        catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
        }*/

        /*try
        {
            var verFactura = db.FacturaRepository.Query(FacturaProjections.Basic).ToList();
            foreach (var datos in verFactura)
            {
                Console.WriteLine(datos.NumeroFactura + " "  + datos.ImporteTotal + " "+ datos.Name + " " + datos.Expr1 + " " + datos.NombreEstado);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }*/

        /*try
        {
            var verFactura = db.FacturaRepository.Query(FacturaProjections.Prueba).ToList();
            foreach (var datos in verFactura)
            {
                Console.WriteLine(datos.NumeroFactura + " " + datos.Importe + " " + datos.Name + " " + datos.NombreEstado + " " + datos.FechaFactura);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }*/


        Console.WriteLine("Presiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}

        
