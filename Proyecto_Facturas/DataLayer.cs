
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
			using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
// using Microsoft.SqlServer.Types;
using System.Runtime.Serialization;

using System.ComponentModel;
using inercya.EntityLite;	
using inercya.EntityLite.Extensions;	

namespace Proyecto_Facturas.Data
{
	[Serializable]
	[DataContract]
	[SqlEntity(BaseTableName="Facturas")]
	public partial class Factura
	{
		private Int32 _idFactura;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, IsAutoincrement=true, IsReadOnly = true, ColumnName ="id_factura", BaseColumnName ="id_factura", BaseTableName = "Facturas" )]		
		public Int32 IdFactura 
		{ 
		    get { return _idFactura; } 
			set 
			{
			    _idFactura = value;
			}
        }

		private String? _numeroFactura;
		[DataMember]
		[SqlField(DbType.String, 150, ColumnName ="numero_factura", BaseColumnName ="numero_factura", BaseTableName = "Facturas" )]		
		public String? NumeroFactura 
		{ 
		    get { return _numeroFactura; } 
			set 
			{
			    _numeroFactura = value;
			}
        }

		private DateTime _fechaFactura;
		[DataMember]
		[SqlField(DbType.DateTime, 8, Precision = 23, Scale=3, ColumnName ="fecha_factura", BaseColumnName ="fecha_factura", BaseTableName = "Facturas" )]		
		public DateTime FechaFactura 
		{ 
		    get { return _fechaFactura; } 
			set 
			{
			    _fechaFactura = value;
			}
        }

		private Int32 _aseguradora;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="aseguradora", BaseColumnName ="aseguradora", BaseTableName = "Facturas" )]		
		public Int32 Aseguradora 
		{ 
		    get { return _aseguradora; } 
			set 
			{
			    _aseguradora = value;
			}
        }

		private Decimal? _importe;
		[DataMember]
		[SqlField(DbType.Decimal, 17, Precision = 19, Scale=2, AllowNull = true, ColumnName ="importe", BaseColumnName ="importe", BaseTableName = "Facturas" )]		
		public Decimal? Importe 
		{ 
		    get { return _importe; } 
			set 
			{
			    _importe = value;
			}
        }

		private Int32? _tipoIva;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, AllowNull = true, ColumnName ="tipo_iva", BaseColumnName ="tipo_iva", BaseTableName = "Facturas" )]		
		public Int32? TipoIva 
		{ 
		    get { return _tipoIva; } 
			set 
			{
			    _tipoIva = value;
			}
        }

		private Decimal? _importeIva;
		[DataMember]
		[SqlField(DbType.Decimal, 17, Precision = 34, Scale=6, AllowNull = true, IsReadOnly = true, ColumnName ="importe_iva", BaseColumnName ="importe_iva", BaseTableName = "Facturas" )]		
		public Decimal? ImporteIva 
		{ 
		    get { return _importeIva; } 
			set 
			{
			    _importeIva = value;
			}
        }

		private Decimal? _importeTotal;
		[DataMember]
		[SqlField(DbType.Decimal, 17, Precision = 35, Scale=6, AllowNull = true, IsReadOnly = true, ColumnName ="importe_total", BaseColumnName ="importe_total", BaseTableName = "Facturas" )]		
		public Decimal? ImporteTotal 
		{ 
		    get { return _importeTotal; } 
			set 
			{
			    _importeTotal = value;
			}
        }

		private Int32 _status;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="status", BaseColumnName ="status", BaseTableName = "Facturas" )]		
		public Int32 Status 
		{ 
		    get { return _status; } 
			set 
			{
			    _status = value;
			}
        }

		private DateTime _creado;
		[DataMember]
		[SqlField(DbType.DateTime, 8, Precision = 23, Scale=3, ColumnName ="creado", BaseColumnName ="creado", BaseTableName = "Facturas" )]		
		public DateTime Creado 
		{ 
		    get { return _creado; } 
			set 
			{
			    _creado = value;
			}
        }

		private Int32 _creadoPor;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="creado_por", BaseColumnName ="creado_por", BaseTableName = "Facturas" )]		
		public Int32 CreadoPor 
		{ 
		    get { return _creadoPor; } 
			set 
			{
			    _creadoPor = value;
			}
        }

		private DateTime _modificado;
		[DataMember]
		[SqlField(DbType.DateTime, 8, Precision = 23, Scale=3, ColumnName ="modificado", BaseColumnName ="modificado", BaseTableName = "Facturas" )]		
		public DateTime Modificado 
		{ 
		    get { return _modificado; } 
			set 
			{
			    _modificado = value;
			}
        }

		private Int32 _modificadoPor;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="modificado_por", BaseColumnName ="modificado_por", BaseTableName = "Facturas" )]		
		public Int32 ModificadoPor 
		{ 
		    get { return _modificadoPor; } 
			set 
			{
			    _modificadoPor = value;
			}
        }

		private String? _nombreUsuario;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="nombreUsuario" )]		
		public String? NombreUsuario 
		{ 
		    get { return _nombreUsuario; } 
			set 
			{
			    _nombreUsuario = value;
			}
        }

		private String? _nombreEstado;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="nombre_estado" )]		
		public String? NombreEstado 
		{ 
		    get { return _nombreEstado; } 
			set 
			{
			    _nombreEstado = value;
			}
        }


	}

	public partial class FacturaRepository : Repository<Factura> 
	{
		public FacturaRepository(DataService DataService) : base(DataService)
		{
		}

		public new FacturacionDataService  DataService  
		{
			get { return (FacturacionDataService) base.DataService; }
			set { base.DataService = value; }
		}

		public Factura Get(string projectionName, Int32 idFactura)
		{
			return ((IRepository<Factura>)this).Get(projectionName, idFactura, FetchMode.UseIdentityMap);
		}

		public Factura Get(string projectionName, Int32 idFactura, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Factura>)this).Get(projectionName, idFactura, fetchMode);
		}

		public Factura Get(Projection projection, Int32 idFactura)
		{
			return ((IRepository<Factura>)this).Get(projection, idFactura, FetchMode.UseIdentityMap);
		}

		public Factura Get(Projection projection, Int32 idFactura, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Factura>)this).Get(projection, idFactura, fetchMode);
		}

		public Factura Get(string projectionName, Int32 idFactura, params string[] fields)
		{
			return ((IRepository<Factura>)this).Get(projectionName, idFactura, fields);
		}

		public Factura Get(Projection projection, Int32 idFactura, params string[] fields)
		{
			return ((IRepository<Factura>)this).Get(projection, idFactura, fields);
		}

		public bool Delete(Int32 idFactura)
		{
			var entity = new Factura { IdFactura = idFactura };
			return this.Delete(entity);
		}

				// asyncrhonous methods

		public System.Threading.Tasks.Task<Factura> GetAsync(string projectionName, Int32 idFactura)
		{
			return ((IRepository<Factura>)this).GetAsync(projectionName, idFactura, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<Factura> GetAsync(string projectionName, Int32 idFactura, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Factura>)this).GetAsync(projectionName, idFactura, fetchMode);
		}

		public System.Threading.Tasks.Task<Factura> GetAsync(Projection projection, Int32 idFactura)
		{
			return ((IRepository<Factura>)this).GetAsync(projection, idFactura, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<Factura> GetAsync(Projection projection, Int32 idFactura, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Factura>)this).GetAsync(projection, idFactura, fetchMode);
		}

		public System.Threading.Tasks.Task<Factura> GetAsync(string projectionName, Int32 idFactura, params string[] fields)
		{
			return ((IRepository<Factura>)this).GetAsync(projectionName, idFactura, fields);
		}

		public System.Threading.Tasks.Task<Factura> GetAsync(Projection projection, Int32 idFactura, params string[] fields)
		{
			return ((IRepository<Factura>)this).GetAsync(projection, idFactura, fields);
		}

		public System.Threading.Tasks.Task<bool> DeleteAsync(Int32 idFactura)
		{
			var entity = new Factura { IdFactura = idFactura };
			return this.DeleteAsync(entity);
		}
		
		public void EliminarLineasFacturaAsociadas(Int32? idFactura)
		{
            var executor = new StoredProcedureExecutor(this.DataService, true)
            {
                GetCommandFunc = () =>
                {
                    var proc =  Proyecto_Facturas.Data.StoredProcedures.CreateEliminarLineasFacturaAsociadasProcedure(this.DataService.Connection, this.DataService.EntityLiteProvider.ParameterPrefix, this.DataService.EntityLiteProvider.DefaultSchema);
					proc.Parameters[this.DataService.EntityLiteProvider.ParameterPrefix + "id_factura"].Value = idFactura == null ? (object) DBNull.Value : idFactura.Value;
                    return proc;
                }
            };

			executor.ExecuteNonQuery();
		}

		public async System.Threading.Tasks.Task EliminarLineasFacturaAsociadasAsync(Int32? idFactura)
		{
            var executor = new StoredProcedureExecutor(this.DataService, true)
            {
                GetCommandFunc = () =>
                {
                    var proc =  Proyecto_Facturas.Data.StoredProcedures.CreateEliminarLineasFacturaAsociadasProcedure(this.DataService.Connection, this.DataService.EntityLiteProvider.ParameterPrefix, this.DataService.EntityLiteProvider.DefaultSchema);
					proc.Parameters[this.DataService.EntityLiteProvider.ParameterPrefix + "id_factura"].Value = idFactura == null ? (object) DBNull.Value : idFactura.Value;
                    return proc;
                }
            };

			await executor.ExecuteNonQueryAsync().ConfigureAwait(false);
		}
	}
	// [Obsolete("Use nameof instead")]
	public static partial class FacturaFields
	{
		public const string IdFactura = "IdFactura";
		public const string NumeroFactura = "NumeroFactura";
		public const string FechaFactura = "FechaFactura";
		public const string Aseguradora = "Aseguradora";
		public const string Importe = "Importe";
		public const string TipoIva = "TipoIva";
		public const string ImporteIva = "ImporteIva";
		public const string ImporteTotal = "ImporteTotal";
		public const string Status = "Status";
		public const string Creado = "Creado";
		public const string CreadoPor = "CreadoPor";
		public const string Modificado = "Modificado";
		public const string ModificadoPor = "ModificadoPor";
		public const string NombreUsuario = "NombreUsuario";
		public const string NombreEstado = "NombreEstado";
	}

	public static partial class FacturaProjections
	{
		public const string BaseTable = "BaseTable";
		public const string Basic = "Basic";
	}
	[Serializable]
	[DataContract]
	[SqlEntity(BaseTableName="Insurances")]
	public partial class Insurance
	{
		private Int32 _idInsurance;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, IsAutoincrement=true, IsReadOnly = true, ColumnName ="id_insurance", BaseColumnName ="id_insurance", BaseTableName = "Insurances" )]		
		public Int32 IdInsurance 
		{ 
		    get { return _idInsurance; } 
			set 
			{
			    _idInsurance = value;
			}
        }

		private String? _name;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="name", BaseColumnName ="name", BaseTableName = "Insurances" )]		
		public String? Name 
		{ 
		    get { return _name; } 
			set 
			{
			    _name = value;
			}
        }


	}

	public partial class InsuranceRepository : Repository<Insurance> 
	{
		public InsuranceRepository(DataService DataService) : base(DataService)
		{
		}

		public new FacturacionDataService  DataService  
		{
			get { return (FacturacionDataService) base.DataService; }
			set { base.DataService = value; }
		}

		public Insurance Get(string projectionName, Int32 idInsurance)
		{
			return ((IRepository<Insurance>)this).Get(projectionName, idInsurance, FetchMode.UseIdentityMap);
		}

		public Insurance Get(string projectionName, Int32 idInsurance, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Insurance>)this).Get(projectionName, idInsurance, fetchMode);
		}

		public Insurance Get(Projection projection, Int32 idInsurance)
		{
			return ((IRepository<Insurance>)this).Get(projection, idInsurance, FetchMode.UseIdentityMap);
		}

		public Insurance Get(Projection projection, Int32 idInsurance, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Insurance>)this).Get(projection, idInsurance, fetchMode);
		}

		public Insurance Get(string projectionName, Int32 idInsurance, params string[] fields)
		{
			return ((IRepository<Insurance>)this).Get(projectionName, idInsurance, fields);
		}

		public Insurance Get(Projection projection, Int32 idInsurance, params string[] fields)
		{
			return ((IRepository<Insurance>)this).Get(projection, idInsurance, fields);
		}

		public bool Delete(Int32 idInsurance)
		{
			var entity = new Insurance { IdInsurance = idInsurance };
			return this.Delete(entity);
		}

				// asyncrhonous methods

		public System.Threading.Tasks.Task<Insurance> GetAsync(string projectionName, Int32 idInsurance)
		{
			return ((IRepository<Insurance>)this).GetAsync(projectionName, idInsurance, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<Insurance> GetAsync(string projectionName, Int32 idInsurance, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Insurance>)this).GetAsync(projectionName, idInsurance, fetchMode);
		}

		public System.Threading.Tasks.Task<Insurance> GetAsync(Projection projection, Int32 idInsurance)
		{
			return ((IRepository<Insurance>)this).GetAsync(projection, idInsurance, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<Insurance> GetAsync(Projection projection, Int32 idInsurance, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Insurance>)this).GetAsync(projection, idInsurance, fetchMode);
		}

		public System.Threading.Tasks.Task<Insurance> GetAsync(string projectionName, Int32 idInsurance, params string[] fields)
		{
			return ((IRepository<Insurance>)this).GetAsync(projectionName, idInsurance, fields);
		}

		public System.Threading.Tasks.Task<Insurance> GetAsync(Projection projection, Int32 idInsurance, params string[] fields)
		{
			return ((IRepository<Insurance>)this).GetAsync(projection, idInsurance, fields);
		}

		public System.Threading.Tasks.Task<bool> DeleteAsync(Int32 idInsurance)
		{
			var entity = new Insurance { IdInsurance = idInsurance };
			return this.DeleteAsync(entity);
		}
			}
	// [Obsolete("Use nameof instead")]
	public static partial class InsuranceFields
	{
		public const string IdInsurance = "IdInsurance";
		public const string Name = "Name";
	}

	public static partial class InsuranceProjections
	{
		public const string BaseTable = "BaseTable";
	}
	[Serializable]
	[DataContract]
	[SqlEntity(BaseTableName="Lineas_Factura")]
	public partial class LineaFactura
	{
		private Int32 _idLineaFactura;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, IsAutoincrement=true, IsReadOnly = true, ColumnName ="id_linea_factura", BaseColumnName ="id_linea_factura", BaseTableName = "Lineas_Factura" )]		
		public Int32 IdLineaFactura 
		{ 
		    get { return _idLineaFactura; } 
			set 
			{
			    _idLineaFactura = value;
			}
        }

		private Int32 _idMaterial;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="id_material", BaseColumnName ="id_material", BaseTableName = "Lineas_Factura" )]		
		public Int32 IdMaterial 
		{ 
		    get { return _idMaterial; } 
			set 
			{
			    _idMaterial = value;
			}
        }

		private Decimal _importe;
		[DataMember]
		[SqlField(DbType.Decimal, 17, Precision = 19, Scale=2, ColumnName ="importe", BaseColumnName ="importe", BaseTableName = "Lineas_Factura" )]		
		public Decimal Importe 
		{ 
		    get { return _importe; } 
			set 
			{
			    _importe = value;
			}
        }

		private Int32 _cantidad;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="cantidad", BaseColumnName ="cantidad", BaseTableName = "Lineas_Factura" )]		
		public Int32 Cantidad 
		{ 
		    get { return _cantidad; } 
			set 
			{
			    _cantidad = value;
			}
        }

		private Decimal? _importeTotal;
		[DataMember]
		[SqlField(DbType.Decimal, 17, Precision = 30, Scale=2, AllowNull = true, IsReadOnly = true, ColumnName ="importe_total", BaseColumnName ="importe_total", BaseTableName = "Lineas_Factura" )]		
		public Decimal? ImporteTotal 
		{ 
		    get { return _importeTotal; } 
			set 
			{
			    _importeTotal = value;
			}
        }

		private DateTime _creado;
		[DataMember]
		[SqlField(DbType.DateTime, 8, Precision = 23, Scale=3, ColumnName ="creado", BaseColumnName ="creado", BaseTableName = "Lineas_Factura" )]		
		public DateTime Creado 
		{ 
		    get { return _creado; } 
			set 
			{
			    _creado = value;
			}
        }

		private Int32 _creadoPor;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="creado_por", BaseColumnName ="creado_por", BaseTableName = "Lineas_Factura" )]		
		public Int32 CreadoPor 
		{ 
		    get { return _creadoPor; } 
			set 
			{
			    _creadoPor = value;
			}
        }

		private DateTime _modificado;
		[DataMember]
		[SqlField(DbType.DateTime, 8, Precision = 23, Scale=3, ColumnName ="modificado", BaseColumnName ="modificado", BaseTableName = "Lineas_Factura" )]		
		public DateTime Modificado 
		{ 
		    get { return _modificado; } 
			set 
			{
			    _modificado = value;
			}
        }

		private Int32 _modificadoPor;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="modificado_por", BaseColumnName ="modificado_por", BaseTableName = "Lineas_Factura" )]		
		public Int32 ModificadoPor 
		{ 
		    get { return _modificadoPor; } 
			set 
			{
			    _modificadoPor = value;
			}
        }

		private Int32 _idFactura;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="id_factura", BaseColumnName ="id_factura", BaseTableName = "Lineas_Factura" )]		
		public Int32 IdFactura 
		{ 
		    get { return _idFactura; } 
			set 
			{
			    _idFactura = value;
			}
        }

		private String? _nombreMaterial;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="nombre_material" )]		
		public String? NombreMaterial 
		{ 
		    get { return _nombreMaterial; } 
			set 
			{
			    _nombreMaterial = value;
			}
        }


	}

	public partial class LineaFacturaRepository : Repository<LineaFactura> 
	{
		public LineaFacturaRepository(DataService DataService) : base(DataService)
		{
		}

		public new FacturacionDataService  DataService  
		{
			get { return (FacturacionDataService) base.DataService; }
			set { base.DataService = value; }
		}

		public LineaFactura Get(string projectionName, Int32 idLineaFactura)
		{
			return ((IRepository<LineaFactura>)this).Get(projectionName, idLineaFactura, FetchMode.UseIdentityMap);
		}

		public LineaFactura Get(string projectionName, Int32 idLineaFactura, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<LineaFactura>)this).Get(projectionName, idLineaFactura, fetchMode);
		}

		public LineaFactura Get(Projection projection, Int32 idLineaFactura)
		{
			return ((IRepository<LineaFactura>)this).Get(projection, idLineaFactura, FetchMode.UseIdentityMap);
		}

		public LineaFactura Get(Projection projection, Int32 idLineaFactura, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<LineaFactura>)this).Get(projection, idLineaFactura, fetchMode);
		}

		public LineaFactura Get(string projectionName, Int32 idLineaFactura, params string[] fields)
		{
			return ((IRepository<LineaFactura>)this).Get(projectionName, idLineaFactura, fields);
		}

		public LineaFactura Get(Projection projection, Int32 idLineaFactura, params string[] fields)
		{
			return ((IRepository<LineaFactura>)this).Get(projection, idLineaFactura, fields);
		}

		public bool Delete(Int32 idLineaFactura)
		{
			var entity = new LineaFactura { IdLineaFactura = idLineaFactura };
			return this.Delete(entity);
		}

				// asyncrhonous methods

		public System.Threading.Tasks.Task<LineaFactura> GetAsync(string projectionName, Int32 idLineaFactura)
		{
			return ((IRepository<LineaFactura>)this).GetAsync(projectionName, idLineaFactura, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<LineaFactura> GetAsync(string projectionName, Int32 idLineaFactura, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<LineaFactura>)this).GetAsync(projectionName, idLineaFactura, fetchMode);
		}

		public System.Threading.Tasks.Task<LineaFactura> GetAsync(Projection projection, Int32 idLineaFactura)
		{
			return ((IRepository<LineaFactura>)this).GetAsync(projection, idLineaFactura, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<LineaFactura> GetAsync(Projection projection, Int32 idLineaFactura, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<LineaFactura>)this).GetAsync(projection, idLineaFactura, fetchMode);
		}

		public System.Threading.Tasks.Task<LineaFactura> GetAsync(string projectionName, Int32 idLineaFactura, params string[] fields)
		{
			return ((IRepository<LineaFactura>)this).GetAsync(projectionName, idLineaFactura, fields);
		}

		public System.Threading.Tasks.Task<LineaFactura> GetAsync(Projection projection, Int32 idLineaFactura, params string[] fields)
		{
			return ((IRepository<LineaFactura>)this).GetAsync(projection, idLineaFactura, fields);
		}

		public System.Threading.Tasks.Task<bool> DeleteAsync(Int32 idLineaFactura)
		{
			var entity = new LineaFactura { IdLineaFactura = idLineaFactura };
			return this.DeleteAsync(entity);
		}
			}
	// [Obsolete("Use nameof instead")]
	public static partial class LineaFacturaFields
	{
		public const string IdLineaFactura = "IdLineaFactura";
		public const string IdMaterial = "IdMaterial";
		public const string Importe = "Importe";
		public const string Cantidad = "Cantidad";
		public const string ImporteTotal = "ImporteTotal";
		public const string Creado = "Creado";
		public const string CreadoPor = "CreadoPor";
		public const string Modificado = "Modificado";
		public const string ModificadoPor = "ModificadoPor";
		public const string IdFactura = "IdFactura";
		public const string NombreMaterial = "NombreMaterial";
	}

	public static partial class LineaFacturaProjections
	{
		public const string BaseTable = "BaseTable";
		public const string Basic = "Basic";
	}
	[Serializable]
	[DataContract]
	[SqlEntity(BaseTableName="Users")]
	public partial class User
	{
		private Int32 _idUser;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, IsAutoincrement=true, IsReadOnly = true, ColumnName ="id_user", BaseColumnName ="id_user", BaseTableName = "Users" )]		
		public Int32 IdUser 
		{ 
		    get { return _idUser; } 
			set 
			{
			    _idUser = value;
			}
        }

		private String? _name;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="name", BaseColumnName ="name", BaseTableName = "Users" )]		
		public String? Name 
		{ 
		    get { return _name; } 
			set 
			{
			    _name = value;
			}
        }

		private String? _email;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="email", BaseColumnName ="email", BaseTableName = "Users" )]		
		public String? Email 
		{ 
		    get { return _email; } 
			set 
			{
			    _email = value;
			}
        }

		private String? _password;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="password", BaseColumnName ="password", BaseTableName = "Users" )]		
		public String? Password 
		{ 
		    get { return _password; } 
			set 
			{
			    _password = value;
			}
        }


	}

	public partial class UserRepository : Repository<User> 
	{
		public UserRepository(DataService DataService) : base(DataService)
		{
		}

		public new FacturacionDataService  DataService  
		{
			get { return (FacturacionDataService) base.DataService; }
			set { base.DataService = value; }
		}

		public User Get(string projectionName, Int32 idUser)
		{
			return ((IRepository<User>)this).Get(projectionName, idUser, FetchMode.UseIdentityMap);
		}

		public User Get(string projectionName, Int32 idUser, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<User>)this).Get(projectionName, idUser, fetchMode);
		}

		public User Get(Projection projection, Int32 idUser)
		{
			return ((IRepository<User>)this).Get(projection, idUser, FetchMode.UseIdentityMap);
		}

		public User Get(Projection projection, Int32 idUser, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<User>)this).Get(projection, idUser, fetchMode);
		}

		public User Get(string projectionName, Int32 idUser, params string[] fields)
		{
			return ((IRepository<User>)this).Get(projectionName, idUser, fields);
		}

		public User Get(Projection projection, Int32 idUser, params string[] fields)
		{
			return ((IRepository<User>)this).Get(projection, idUser, fields);
		}

		public bool Delete(Int32 idUser)
		{
			var entity = new User { IdUser = idUser };
			return this.Delete(entity);
		}

				// asyncrhonous methods

		public System.Threading.Tasks.Task<User> GetAsync(string projectionName, Int32 idUser)
		{
			return ((IRepository<User>)this).GetAsync(projectionName, idUser, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<User> GetAsync(string projectionName, Int32 idUser, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<User>)this).GetAsync(projectionName, idUser, fetchMode);
		}

		public System.Threading.Tasks.Task<User> GetAsync(Projection projection, Int32 idUser)
		{
			return ((IRepository<User>)this).GetAsync(projection, idUser, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<User> GetAsync(Projection projection, Int32 idUser, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<User>)this).GetAsync(projection, idUser, fetchMode);
		}

		public System.Threading.Tasks.Task<User> GetAsync(string projectionName, Int32 idUser, params string[] fields)
		{
			return ((IRepository<User>)this).GetAsync(projectionName, idUser, fields);
		}

		public System.Threading.Tasks.Task<User> GetAsync(Projection projection, Int32 idUser, params string[] fields)
		{
			return ((IRepository<User>)this).GetAsync(projection, idUser, fields);
		}

		public System.Threading.Tasks.Task<bool> DeleteAsync(Int32 idUser)
		{
			var entity = new User { IdUser = idUser };
			return this.DeleteAsync(entity);
		}
			}
	// [Obsolete("Use nameof instead")]
	public static partial class UserFields
	{
		public const string IdUser = "IdUser";
		public const string Name = "Name";
		public const string Email = "Email";
		public const string Password = "Password";
	}

	public static partial class UserProjections
	{
		public const string BaseTable = "BaseTable";
	}
	[Serializable]
	[DataContract]
	[SqlEntity(BaseTableName="Materials")]
	public partial class Material
	{
		private Int32 _idMaterial;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, IsAutoincrement=true, IsReadOnly = true, ColumnName ="id_material", BaseColumnName ="id_material", BaseTableName = "Materials" )]		
		public Int32 IdMaterial 
		{ 
		    get { return _idMaterial; } 
			set 
			{
			    _idMaterial = value;
			}
        }

		private String? _name;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="name", BaseColumnName ="name", BaseTableName = "Materials" )]		
		public String? Name 
		{ 
		    get { return _name; } 
			set 
			{
			    _name = value;
			}
        }


	}

	public partial class MaterialRepository : Repository<Material> 
	{
		public MaterialRepository(DataService DataService) : base(DataService)
		{
		}

		public new FacturacionDataService  DataService  
		{
			get { return (FacturacionDataService) base.DataService; }
			set { base.DataService = value; }
		}

		public Material Get(string projectionName, Int32 idMaterial)
		{
			return ((IRepository<Material>)this).Get(projectionName, idMaterial, FetchMode.UseIdentityMap);
		}

		public Material Get(string projectionName, Int32 idMaterial, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Material>)this).Get(projectionName, idMaterial, fetchMode);
		}

		public Material Get(Projection projection, Int32 idMaterial)
		{
			return ((IRepository<Material>)this).Get(projection, idMaterial, FetchMode.UseIdentityMap);
		}

		public Material Get(Projection projection, Int32 idMaterial, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Material>)this).Get(projection, idMaterial, fetchMode);
		}

		public Material Get(string projectionName, Int32 idMaterial, params string[] fields)
		{
			return ((IRepository<Material>)this).Get(projectionName, idMaterial, fields);
		}

		public Material Get(Projection projection, Int32 idMaterial, params string[] fields)
		{
			return ((IRepository<Material>)this).Get(projection, idMaterial, fields);
		}

		public bool Delete(Int32 idMaterial)
		{
			var entity = new Material { IdMaterial = idMaterial };
			return this.Delete(entity);
		}

				// asyncrhonous methods

		public System.Threading.Tasks.Task<Material> GetAsync(string projectionName, Int32 idMaterial)
		{
			return ((IRepository<Material>)this).GetAsync(projectionName, idMaterial, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<Material> GetAsync(string projectionName, Int32 idMaterial, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Material>)this).GetAsync(projectionName, idMaterial, fetchMode);
		}

		public System.Threading.Tasks.Task<Material> GetAsync(Projection projection, Int32 idMaterial)
		{
			return ((IRepository<Material>)this).GetAsync(projection, idMaterial, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<Material> GetAsync(Projection projection, Int32 idMaterial, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Material>)this).GetAsync(projection, idMaterial, fetchMode);
		}

		public System.Threading.Tasks.Task<Material> GetAsync(string projectionName, Int32 idMaterial, params string[] fields)
		{
			return ((IRepository<Material>)this).GetAsync(projectionName, idMaterial, fields);
		}

		public System.Threading.Tasks.Task<Material> GetAsync(Projection projection, Int32 idMaterial, params string[] fields)
		{
			return ((IRepository<Material>)this).GetAsync(projection, idMaterial, fields);
		}

		public System.Threading.Tasks.Task<bool> DeleteAsync(Int32 idMaterial)
		{
			var entity = new Material { IdMaterial = idMaterial };
			return this.DeleteAsync(entity);
		}
			}
	// [Obsolete("Use nameof instead")]
	public static partial class MaterialFields
	{
		public const string IdMaterial = "IdMaterial";
		public const string Name = "Name";
	}

	public static partial class MaterialProjections
	{
		public const string BaseTable = "BaseTable";
	}
	[Serializable]
	[DataContract]
	[SqlEntity(BaseTableName="Master_data")]
	public partial class Master_data
	{
		private Int32 _idEstado;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, IsAutoincrement=true, IsReadOnly = true, ColumnName ="id_estado", BaseColumnName ="id_estado", BaseTableName = "Master_data" )]		
		public Int32 IdEstado 
		{ 
		    get { return _idEstado; } 
			set 
			{
			    _idEstado = value;
			}
        }

		private String? _nombreEstado;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="nombre_estado", BaseColumnName ="nombre_estado", BaseTableName = "Master_data" )]		
		public String? NombreEstado 
		{ 
		    get { return _nombreEstado; } 
			set 
			{
			    _nombreEstado = value;
			}
        }


	}

	public partial class Master_dataRepository : Repository<Master_data> 
	{
		public Master_dataRepository(DataService DataService) : base(DataService)
		{
		}

		public new FacturacionDataService  DataService  
		{
			get { return (FacturacionDataService) base.DataService; }
			set { base.DataService = value; }
		}

		public Master_data Get(string projectionName, Int32 idEstado)
		{
			return ((IRepository<Master_data>)this).Get(projectionName, idEstado, FetchMode.UseIdentityMap);
		}

		public Master_data Get(string projectionName, Int32 idEstado, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Master_data>)this).Get(projectionName, idEstado, fetchMode);
		}

		public Master_data Get(Projection projection, Int32 idEstado)
		{
			return ((IRepository<Master_data>)this).Get(projection, idEstado, FetchMode.UseIdentityMap);
		}

		public Master_data Get(Projection projection, Int32 idEstado, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Master_data>)this).Get(projection, idEstado, fetchMode);
		}

		public Master_data Get(string projectionName, Int32 idEstado, params string[] fields)
		{
			return ((IRepository<Master_data>)this).Get(projectionName, idEstado, fields);
		}

		public Master_data Get(Projection projection, Int32 idEstado, params string[] fields)
		{
			return ((IRepository<Master_data>)this).Get(projection, idEstado, fields);
		}

		public bool Delete(Int32 idEstado)
		{
			var entity = new Master_data { IdEstado = idEstado };
			return this.Delete(entity);
		}

				// asyncrhonous methods

		public System.Threading.Tasks.Task<Master_data> GetAsync(string projectionName, Int32 idEstado)
		{
			return ((IRepository<Master_data>)this).GetAsync(projectionName, idEstado, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<Master_data> GetAsync(string projectionName, Int32 idEstado, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Master_data>)this).GetAsync(projectionName, idEstado, fetchMode);
		}

		public System.Threading.Tasks.Task<Master_data> GetAsync(Projection projection, Int32 idEstado)
		{
			return ((IRepository<Master_data>)this).GetAsync(projection, idEstado, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<Master_data> GetAsync(Projection projection, Int32 idEstado, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Master_data>)this).GetAsync(projection, idEstado, fetchMode);
		}

		public System.Threading.Tasks.Task<Master_data> GetAsync(string projectionName, Int32 idEstado, params string[] fields)
		{
			return ((IRepository<Master_data>)this).GetAsync(projectionName, idEstado, fields);
		}

		public System.Threading.Tasks.Task<Master_data> GetAsync(Projection projection, Int32 idEstado, params string[] fields)
		{
			return ((IRepository<Master_data>)this).GetAsync(projection, idEstado, fields);
		}

		public System.Threading.Tasks.Task<bool> DeleteAsync(Int32 idEstado)
		{
			var entity = new Master_data { IdEstado = idEstado };
			return this.DeleteAsync(entity);
		}
			}
	// [Obsolete("Use nameof instead")]
	public static partial class Master_dataFields
	{
		public const string IdEstado = "IdEstado";
		public const string NombreEstado = "NombreEstado";
	}

	public static partial class Master_dataProjections
	{
		public const string BaseTable = "BaseTable";
	}
	[Serializable]
	[DataContract]
	[SqlEntity(BaseTableName="FacturaSimple")]
	public partial class FacturaSimple
	{
		private Int32 _idFactura;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, ColumnName ="id_factura" )]		
		public Int32 IdFactura 
		{ 
		    get { return _idFactura; } 
			set 
			{
			    _idFactura = value;
			}
        }

		private String? _numeroFactura;
		[DataMember]
		[SqlField(DbType.String, 150, ColumnName ="numero_factura" )]		
		public String? NumeroFactura 
		{ 
		    get { return _numeroFactura; } 
			set 
			{
			    _numeroFactura = value;
			}
        }

		private String? _nombreAseguradora;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="nombreAseguradora" )]		
		public String? NombreAseguradora 
		{ 
		    get { return _nombreAseguradora; } 
			set 
			{
			    _nombreAseguradora = value;
			}
        }

		private Decimal? _importe;
		[DataMember]
		[SqlField(DbType.Decimal, 17, Precision = 19, Scale=2, AllowNull = true, ColumnName ="importe" )]		
		public Decimal? Importe 
		{ 
		    get { return _importe; } 
			set 
			{
			    _importe = value;
			}
        }

		private Int32? _tipoIva;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, AllowNull = true, ColumnName ="tipo_iva" )]		
		public Int32? TipoIva 
		{ 
		    get { return _tipoIva; } 
			set 
			{
			    _tipoIva = value;
			}
        }

		private Decimal? _importeTotal;
		[DataMember]
		[SqlField(DbType.Decimal, 17, Precision = 35, Scale=6, AllowNull = true, IsReadOnly = true, ColumnName ="importe_total" )]		
		public Decimal? ImporteTotal 
		{ 
		    get { return _importeTotal; } 
			set 
			{
			    _importeTotal = value;
			}
        }


	}

	public partial class FacturaSimpleRepository : Repository<FacturaSimple> 
	{
		public FacturaSimpleRepository(DataService DataService) : base(DataService)
		{
		}

		public new FacturacionDataService  DataService  
		{
			get { return (FacturacionDataService) base.DataService; }
			set { base.DataService = value; }
		}

	}
	// [Obsolete("Use nameof instead")]
	public static partial class FacturaSimpleFields
	{
		public const string IdFactura = "IdFactura";
		public const string NumeroFactura = "NumeroFactura";
		public const string NombreAseguradora = "NombreAseguradora";
		public const string Importe = "Importe";
		public const string TipoIva = "TipoIva";
		public const string ImporteTotal = "ImporteTotal";
	}

	public static partial class FacturaSimpleProjections
	{
		public const string BaseTable = "BaseTable";
	}
	[Serializable]
	[DataContract]
	[SqlEntity(BaseTableName="LineaSimple")]
	public partial class LineaSimple
	{
		private Int32 _idLineaFactura;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, ColumnName ="id_linea_factura" )]		
		public Int32 IdLineaFactura 
		{ 
		    get { return _idLineaFactura; } 
			set 
			{
			    _idLineaFactura = value;
			}
        }

		private Int32 _cantidad;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="cantidad" )]		
		public Int32 Cantidad 
		{ 
		    get { return _cantidad; } 
			set 
			{
			    _cantidad = value;
			}
        }

		private Decimal _importe;
		[DataMember]
		[SqlField(DbType.Decimal, 17, Precision = 19, Scale=2, ColumnName ="importe" )]		
		public Decimal Importe 
		{ 
		    get { return _importe; } 
			set 
			{
			    _importe = value;
			}
        }

		private Decimal? _importeTotal;
		[DataMember]
		[SqlField(DbType.Decimal, 17, Precision = 30, Scale=2, AllowNull = true, IsReadOnly = true, ColumnName ="importe_total" )]		
		public Decimal? ImporteTotal 
		{ 
		    get { return _importeTotal; } 
			set 
			{
			    _importeTotal = value;
			}
        }

		private String? _nombreMaterial;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="nombre_material" )]		
		public String? NombreMaterial 
		{ 
		    get { return _nombreMaterial; } 
			set 
			{
			    _nombreMaterial = value;
			}
        }

		private Int32 _idFactura;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="id_factura" )]		
		public Int32 IdFactura 
		{ 
		    get { return _idFactura; } 
			set 
			{
			    _idFactura = value;
			}
        }


	}

	public partial class LineaSimpleRepository : Repository<LineaSimple> 
	{
		public LineaSimpleRepository(DataService DataService) : base(DataService)
		{
		}

		public new FacturacionDataService  DataService  
		{
			get { return (FacturacionDataService) base.DataService; }
			set { base.DataService = value; }
		}

	}
	// [Obsolete("Use nameof instead")]
	public static partial class LineaSimpleFields
	{
		public const string IdLineaFactura = "IdLineaFactura";
		public const string Cantidad = "Cantidad";
		public const string Importe = "Importe";
		public const string ImporteTotal = "ImporteTotal";
		public const string NombreMaterial = "NombreMaterial";
		public const string IdFactura = "IdFactura";
	}

	public static partial class LineaSimpleProjections
	{
		public const string BaseTable = "BaseTable";
	}
}

namespace Proyecto_Facturas.Data
{
	public partial class FacturacionDataService : DataService
	{
		partial void OnCreated();

		private void Init()
		{
			EntityNameToEntityViewTransform = TextTransform.None;
			EntityLiteProvider.DefaultSchema = "dbo";
			AuditDateTimeKind = DateTimeKind.Utc;
			OnCreated();
		}

        public FacturacionDataService() : base()
        {
			Init();
        }

        public FacturacionDataService(string connectionString) : base(System.Data.SqlClient.SqlClientFactory.Instance, connectionString)
        {
			Init();
        }


		private Proyecto_Facturas.Data.FacturaRepository _FacturaRepository;
		public Proyecto_Facturas.Data.FacturaRepository FacturaRepository
		{
			get 
			{
				if ( _FacturaRepository == null)
				{
					_FacturaRepository = new Proyecto_Facturas.Data.FacturaRepository(this);
				}
				return _FacturaRepository;
			}
		}

		private Proyecto_Facturas.Data.InsuranceRepository _InsuranceRepository;
		public Proyecto_Facturas.Data.InsuranceRepository InsuranceRepository
		{
			get 
			{
				if ( _InsuranceRepository == null)
				{
					_InsuranceRepository = new Proyecto_Facturas.Data.InsuranceRepository(this);
				}
				return _InsuranceRepository;
			}
		}

		private Proyecto_Facturas.Data.LineaFacturaRepository _LineaFacturaRepository;
		public Proyecto_Facturas.Data.LineaFacturaRepository LineaFacturaRepository
		{
			get 
			{
				if ( _LineaFacturaRepository == null)
				{
					_LineaFacturaRepository = new Proyecto_Facturas.Data.LineaFacturaRepository(this);
				}
				return _LineaFacturaRepository;
			}
		}

		private Proyecto_Facturas.Data.UserRepository _UserRepository;
		public Proyecto_Facturas.Data.UserRepository UserRepository
		{
			get 
			{
				if ( _UserRepository == null)
				{
					_UserRepository = new Proyecto_Facturas.Data.UserRepository(this);
				}
				return _UserRepository;
			}
		}

		private Proyecto_Facturas.Data.MaterialRepository _MaterialRepository;
		public Proyecto_Facturas.Data.MaterialRepository MaterialRepository
		{
			get 
			{
				if ( _MaterialRepository == null)
				{
					_MaterialRepository = new Proyecto_Facturas.Data.MaterialRepository(this);
				}
				return _MaterialRepository;
			}
		}

		private Proyecto_Facturas.Data.Master_dataRepository _Master_dataRepository;
		public Proyecto_Facturas.Data.Master_dataRepository Master_dataRepository
		{
			get 
			{
				if ( _Master_dataRepository == null)
				{
					_Master_dataRepository = new Proyecto_Facturas.Data.Master_dataRepository(this);
				}
				return _Master_dataRepository;
			}
		}

		private Proyecto_Facturas.Data.FacturaSimpleRepository _FacturaSimpleRepository;
		public Proyecto_Facturas.Data.FacturaSimpleRepository FacturaSimpleRepository
		{
			get 
			{
				if ( _FacturaSimpleRepository == null)
				{
					_FacturaSimpleRepository = new Proyecto_Facturas.Data.FacturaSimpleRepository(this);
				}
				return _FacturaSimpleRepository;
			}
		}

		private Proyecto_Facturas.Data.LineaSimpleRepository _LineaSimpleRepository;
		public Proyecto_Facturas.Data.LineaSimpleRepository LineaSimpleRepository
		{
			get 
			{
				if ( _LineaSimpleRepository == null)
				{
					_LineaSimpleRepository = new Proyecto_Facturas.Data.LineaSimpleRepository(this);
				}
				return _LineaSimpleRepository;
			}
		}
	}
}
namespace Proyecto_Facturas.Data
{
	public static partial class StoredProcedures
	{
		public static DbCommand CreateEliminarLineasFacturaAsociadasProcedure(DbConnection connection, string parameterPrefix, string schema = "")
		{
			var cmd = connection.CreateCommand();
			cmd.CommandText = string.IsNullOrEmpty(schema) ? "EliminarLineasFacturaAsociadas" : schema + "." + "EliminarLineasFacturaAsociadas";
			cmd.CommandType = CommandType.StoredProcedure;
			IDbDataParameter p = null;

			p = cmd.CreateParameter();
			p.ParameterName = parameterPrefix + "RETURN_VALUE";
			p.DbType = DbType.Int32;
            p.Direction = ParameterDirection.ReturnValue;
			p.SourceColumn = "RETURN_VALUE";
			cmd.Parameters.Add(p);

			p = cmd.CreateParameter();
			p.ParameterName = parameterPrefix + "id_factura";
			p.DbType = DbType.Int32;
            p.Direction = ParameterDirection.Input;
			p.SourceColumn = "id_factura";
			cmd.Parameters.Add(p);

			return cmd;
		}

	}
}
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor
			