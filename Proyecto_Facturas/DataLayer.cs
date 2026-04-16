
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

		private Int32 _clientId;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="client_id", BaseColumnName ="client_id", BaseTableName = "Facturas" )]		
		public Int32 ClientId 
		{ 
		    get { return _clientId; } 
			set 
			{
			    _clientId = value;
			}
        }

		private Int32 _entityRowVersion;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="entity_row_version", BaseColumnName ="entity_row_version", BaseTableName = "Facturas" )]		
		public Int32 EntityRowVersion 
		{ 
		    get { return _entityRowVersion; } 
			set 
			{
			    _entityRowVersion = value;
			}
        }

		private String? _insuranceName;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="insurance_name" )]		
		public String? InsuranceName 
		{ 
		    get { return _insuranceName; } 
			set 
			{
			    _insuranceName = value;
			}
        }

		private String? _statusName;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="status_name" )]		
		public String? StatusName 
		{ 
		    get { return _statusName; } 
			set 
			{
			    _statusName = value;
			}
        }

		private String? _userName;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="user_name" )]		
		public String? UserName 
		{ 
		    get { return _userName; } 
			set 
			{
			    _userName = value;
			}
        }

		private String? _clientCif;
		[DataMember]
		[SqlField(DbType.String, 9, ColumnName ="client_cif" )]		
		public String? ClientCif 
		{ 
		    get { return _clientCif; } 
			set 
			{
			    _clientCif = value;
			}
        }

		private String? _clientLegalName;
		[DataMember]
		[SqlField(DbType.String, 150, ColumnName ="client_legal_name" )]		
		public String? ClientLegalName 
		{ 
		    get { return _clientLegalName; } 
			set 
			{
			    _clientLegalName = value;
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
		public const string ClientId = "ClientId";
		public const string EntityRowVersion = "EntityRowVersion";
		public const string InsuranceName = "InsuranceName";
		public const string StatusName = "StatusName";
		public const string UserName = "UserName";
		public const string ClientCif = "ClientCif";
		public const string ClientLegalName = "ClientLegalName";
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

		private Int32 _productId;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="product_id", BaseColumnName ="product_id", BaseTableName = "Lineas_Factura" )]		
		public Int32 ProductId 
		{ 
		    get { return _productId; } 
			set 
			{
			    _productId = value;
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

		private String? _productName;
		[DataMember]
		[SqlField(DbType.String, 150, ColumnName ="product_name" )]		
		public String? ProductName 
		{ 
		    get { return _productName; } 
			set 
			{
			    _productName = value;
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
		public const string ProductId = "ProductId";
		public const string Importe = "Importe";
		public const string Cantidad = "Cantidad";
		public const string ImporteTotal = "ImporteTotal";
		public const string Creado = "Creado";
		public const string CreadoPor = "CreadoPor";
		public const string Modificado = "Modificado";
		public const string ModificadoPor = "ModificadoPor";
		public const string IdFactura = "IdFactura";
		public const string ProductName = "ProductName";
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
		[SqlField(DbType.String, 250, ColumnName ="password", BaseColumnName ="password", BaseTableName = "Users" )]		
		public String? Password 
		{ 
		    get { return _password; } 
			set 
			{
			    _password = value;
			}
        }

		private String? _role;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="role", BaseColumnName ="role", BaseTableName = "Users" )]		
		public String? Role 
		{ 
		    get { return _role; } 
			set 
			{
			    _role = value;
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
		
		public void DeleteUserWithInvoices(Int32? userId)
		{
            var executor = new StoredProcedureExecutor(this.DataService, true)
            {
                GetCommandFunc = () =>
                {
                    var proc =  Proyecto_Facturas.Data.StoredProcedures.CreateDeleteUserWithInvoicesProcedure(this.DataService.Connection, this.DataService.EntityLiteProvider.ParameterPrefix, this.DataService.EntityLiteProvider.DefaultSchema);
					proc.Parameters[this.DataService.EntityLiteProvider.ParameterPrefix + "user_id"].Value = userId == null ? (object) DBNull.Value : userId.Value;
                    return proc;
                }
            };

			executor.ExecuteNonQuery();
		}

		public async System.Threading.Tasks.Task DeleteUserWithInvoicesAsync(Int32? userId)
		{
            var executor = new StoredProcedureExecutor(this.DataService, true)
            {
                GetCommandFunc = () =>
                {
                    var proc =  Proyecto_Facturas.Data.StoredProcedures.CreateDeleteUserWithInvoicesProcedure(this.DataService.Connection, this.DataService.EntityLiteProvider.ParameterPrefix, this.DataService.EntityLiteProvider.DefaultSchema);
					proc.Parameters[this.DataService.EntityLiteProvider.ParameterPrefix + "user_id"].Value = userId == null ? (object) DBNull.Value : userId.Value;
                    return proc;
                }
            };

			await executor.ExecuteNonQueryAsync().ConfigureAwait(false);
		}
	}
	// [Obsolete("Use nameof instead")]
	public static partial class UserFields
	{
		public const string IdUser = "IdUser";
		public const string Name = "Name";
		public const string Email = "Email";
		public const string Password = "Password";
		public const string Role = "Role";
	}

	public static partial class UserProjections
	{
		public const string BaseTable = "BaseTable";
	}
	[Serializable]
	[DataContract]
	[SqlEntity(BaseTableName="Products")]
	public partial class Product
	{
		private Int32 _productId;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, IsAutoincrement=true, IsReadOnly = true, ColumnName ="product_id", BaseColumnName ="product_id", BaseTableName = "Products" )]		
		public Int32 ProductId 
		{ 
		    get { return _productId; } 
			set 
			{
			    _productId = value;
			}
        }

		private String? _name;
		[DataMember]
		[SqlField(DbType.String, 150, ColumnName ="name", BaseColumnName ="name", BaseTableName = "Products" )]		
		public String? Name 
		{ 
		    get { return _name; } 
			set 
			{
			    _name = value;
			}
        }

		private Decimal? _defaultPrice;
		[DataMember]
		[SqlField(DbType.Decimal, 17, Precision = 19, Scale=2, AllowNull = true, ColumnName ="default_price", BaseColumnName ="default_price", BaseTableName = "Products" )]		
		public Decimal? DefaultPrice 
		{ 
		    get { return _defaultPrice; } 
			set 
			{
			    _defaultPrice = value;
			}
        }

		private Boolean _active;
		[DataMember]
		[SqlField(DbType.Boolean, 1, ColumnName ="active", BaseColumnName ="active", BaseTableName = "Products" )]		
		public Boolean Active 
		{ 
		    get { return _active; } 
			set 
			{
			    _active = value;
			}
        }

		private Int32 _userId;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, ColumnName ="user_id", BaseColumnName ="user_id", BaseTableName = "Products" )]		
		public Int32 UserId 
		{ 
		    get { return _userId; } 
			set 
			{
			    _userId = value;
			}
        }

		private String? _description;
		[DataMember]
		[SqlField(DbType.String, 150, ColumnName ="description", BaseColumnName ="description", BaseTableName = "Products" )]		
		public String? Description 
		{ 
		    get { return _description; } 
			set 
			{
			    _description = value;
			}
        }


	}

	public partial class ProductRepository : Repository<Product> 
	{
		public ProductRepository(DataService DataService) : base(DataService)
		{
		}

		public new FacturacionDataService  DataService  
		{
			get { return (FacturacionDataService) base.DataService; }
			set { base.DataService = value; }
		}

		public Product Get(string projectionName, Int32 productId)
		{
			return ((IRepository<Product>)this).Get(projectionName, productId, FetchMode.UseIdentityMap);
		}

		public Product Get(string projectionName, Int32 productId, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Product>)this).Get(projectionName, productId, fetchMode);
		}

		public Product Get(Projection projection, Int32 productId)
		{
			return ((IRepository<Product>)this).Get(projection, productId, FetchMode.UseIdentityMap);
		}

		public Product Get(Projection projection, Int32 productId, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Product>)this).Get(projection, productId, fetchMode);
		}

		public Product Get(string projectionName, Int32 productId, params string[] fields)
		{
			return ((IRepository<Product>)this).Get(projectionName, productId, fields);
		}

		public Product Get(Projection projection, Int32 productId, params string[] fields)
		{
			return ((IRepository<Product>)this).Get(projection, productId, fields);
		}

		public bool Delete(Int32 productId)
		{
			var entity = new Product { ProductId = productId };
			return this.Delete(entity);
		}

				// asyncrhonous methods

		public System.Threading.Tasks.Task<Product> GetAsync(string projectionName, Int32 productId)
		{
			return ((IRepository<Product>)this).GetAsync(projectionName, productId, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<Product> GetAsync(string projectionName, Int32 productId, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Product>)this).GetAsync(projectionName, productId, fetchMode);
		}

		public System.Threading.Tasks.Task<Product> GetAsync(Projection projection, Int32 productId)
		{
			return ((IRepository<Product>)this).GetAsync(projection, productId, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<Product> GetAsync(Projection projection, Int32 productId, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Product>)this).GetAsync(projection, productId, fetchMode);
		}

		public System.Threading.Tasks.Task<Product> GetAsync(string projectionName, Int32 productId, params string[] fields)
		{
			return ((IRepository<Product>)this).GetAsync(projectionName, productId, fields);
		}

		public System.Threading.Tasks.Task<Product> GetAsync(Projection projection, Int32 productId, params string[] fields)
		{
			return ((IRepository<Product>)this).GetAsync(projection, productId, fields);
		}

		public System.Threading.Tasks.Task<bool> DeleteAsync(Int32 productId)
		{
			var entity = new Product { ProductId = productId };
			return this.DeleteAsync(entity);
		}
			}
	// [Obsolete("Use nameof instead")]
	public static partial class ProductFields
	{
		public const string ProductId = "ProductId";
		public const string Name = "Name";
		public const string DefaultPrice = "DefaultPrice";
		public const string Active = "Active";
		public const string UserId = "UserId";
		public const string Description = "Description";
	}

	public static partial class ProductProjections
	{
		public const string BaseTable = "BaseTable";
	}
	[Serializable]
	[DataContract]
	[SqlEntity(BaseTableName="Invoice_Status")]
	public partial class InvoiceStatus
	{
		private Int32 _statusId;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, ColumnName ="status_id", BaseColumnName ="status_id", BaseTableName = "Invoice_Status" )]		
		public Int32 StatusId 
		{ 
		    get { return _statusId; } 
			set 
			{
			    _statusId = value;
			}
        }

		private String? _statusName;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="status_name", BaseColumnName ="status_name", BaseTableName = "Invoice_Status" )]		
		public String? StatusName 
		{ 
		    get { return _statusName; } 
			set 
			{
			    _statusName = value;
			}
        }


	}

	public partial class InvoiceStatusRepository : Repository<InvoiceStatus> 
	{
		public InvoiceStatusRepository(DataService DataService) : base(DataService)
		{
		}

		public new FacturacionDataService  DataService  
		{
			get { return (FacturacionDataService) base.DataService; }
			set { base.DataService = value; }
		}

		public InvoiceStatus Get(string projectionName, Int32 statusId)
		{
			return ((IRepository<InvoiceStatus>)this).Get(projectionName, statusId, FetchMode.UseIdentityMap);
		}

		public InvoiceStatus Get(string projectionName, Int32 statusId, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<InvoiceStatus>)this).Get(projectionName, statusId, fetchMode);
		}

		public InvoiceStatus Get(Projection projection, Int32 statusId)
		{
			return ((IRepository<InvoiceStatus>)this).Get(projection, statusId, FetchMode.UseIdentityMap);
		}

		public InvoiceStatus Get(Projection projection, Int32 statusId, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<InvoiceStatus>)this).Get(projection, statusId, fetchMode);
		}

		public InvoiceStatus Get(string projectionName, Int32 statusId, params string[] fields)
		{
			return ((IRepository<InvoiceStatus>)this).Get(projectionName, statusId, fields);
		}

		public InvoiceStatus Get(Projection projection, Int32 statusId, params string[] fields)
		{
			return ((IRepository<InvoiceStatus>)this).Get(projection, statusId, fields);
		}

		public bool Delete(Int32 statusId)
		{
			var entity = new InvoiceStatus { StatusId = statusId };
			return this.Delete(entity);
		}

				// asyncrhonous methods

		public System.Threading.Tasks.Task<InvoiceStatus> GetAsync(string projectionName, Int32 statusId)
		{
			return ((IRepository<InvoiceStatus>)this).GetAsync(projectionName, statusId, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<InvoiceStatus> GetAsync(string projectionName, Int32 statusId, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<InvoiceStatus>)this).GetAsync(projectionName, statusId, fetchMode);
		}

		public System.Threading.Tasks.Task<InvoiceStatus> GetAsync(Projection projection, Int32 statusId)
		{
			return ((IRepository<InvoiceStatus>)this).GetAsync(projection, statusId, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<InvoiceStatus> GetAsync(Projection projection, Int32 statusId, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<InvoiceStatus>)this).GetAsync(projection, statusId, fetchMode);
		}

		public System.Threading.Tasks.Task<InvoiceStatus> GetAsync(string projectionName, Int32 statusId, params string[] fields)
		{
			return ((IRepository<InvoiceStatus>)this).GetAsync(projectionName, statusId, fields);
		}

		public System.Threading.Tasks.Task<InvoiceStatus> GetAsync(Projection projection, Int32 statusId, params string[] fields)
		{
			return ((IRepository<InvoiceStatus>)this).GetAsync(projection, statusId, fields);
		}

		public System.Threading.Tasks.Task<bool> DeleteAsync(Int32 statusId)
		{
			var entity = new InvoiceStatus { StatusId = statusId };
			return this.DeleteAsync(entity);
		}
			}
	// [Obsolete("Use nameof instead")]
	public static partial class InvoiceStatusFields
	{
		public const string StatusId = "StatusId";
		public const string StatusName = "StatusName";
	}

	public static partial class InvoiceStatusProjections
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
	[SqlEntity(BaseTableName="Clients")]
	public partial class Client
	{
		private Int32 _clientId;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, IsAutoincrement=true, IsReadOnly = true, ColumnName ="client_id", BaseColumnName ="client_id", BaseTableName = "Clients" )]		
		public Int32 ClientId 
		{ 
		    get { return _clientId; } 
			set 
			{
			    _clientId = value;
			}
        }

		private String? _commercialName;
		[DataMember]
		[SqlField(DbType.String, 150, ColumnName ="commercial_name", BaseColumnName ="commercial_name", BaseTableName = "Clients" )]		
		public String? CommercialName 
		{ 
		    get { return _commercialName; } 
			set 
			{
			    _commercialName = value;
			}
        }

		private String? _legalName;
		[DataMember]
		[SqlField(DbType.String, 150, ColumnName ="legal_name", BaseColumnName ="legal_name", BaseTableName = "Clients" )]		
		public String? LegalName 
		{ 
		    get { return _legalName; } 
			set 
			{
			    _legalName = value;
			}
        }

		private String? _cif;
		[DataMember]
		[SqlField(DbType.String, 9, ColumnName ="cif", BaseColumnName ="cif", BaseTableName = "Clients" )]		
		public String? Cif 
		{ 
		    get { return _cif; } 
			set 
			{
			    _cif = value;
			}
        }

		private String? _email;
		[DataMember]
		[SqlField(DbType.String, 50, ColumnName ="email", BaseColumnName ="email", BaseTableName = "Clients" )]		
		public String? Email 
		{ 
		    get { return _email; } 
			set 
			{
			    _email = value;
			}
        }

		private String? _phone;
		[DataMember]
		[SqlField(DbType.String, 20, ColumnName ="phone", BaseColumnName ="phone", BaseTableName = "Clients" )]		
		public String? Phone 
		{ 
		    get { return _phone; } 
			set 
			{
			    _phone = value;
			}
        }

		private String? _address;
		[DataMember]
		[SqlField(DbType.String, 150, ColumnName ="address", BaseColumnName ="address", BaseTableName = "Clients" )]		
		public String? Address 
		{ 
		    get { return _address; } 
			set 
			{
			    _address = value;
			}
        }

		private DateTime _creationDate;
		[DataMember]
		[SqlField(DbType.DateTime, 8, Precision = 23, Scale=3, ColumnName ="creation_date", BaseColumnName ="creation_date", BaseTableName = "Clients" )]		
		public DateTime CreationDate 
		{ 
		    get { return _creationDate; } 
			set 
			{
			    _creationDate = value;
			}
        }


	}

	public partial class ClientRepository : Repository<Client> 
	{
		public ClientRepository(DataService DataService) : base(DataService)
		{
		}

		public new FacturacionDataService  DataService  
		{
			get { return (FacturacionDataService) base.DataService; }
			set { base.DataService = value; }
		}

		public Client Get(string projectionName, Int32 clientId)
		{
			return ((IRepository<Client>)this).Get(projectionName, clientId, FetchMode.UseIdentityMap);
		}

		public Client Get(string projectionName, Int32 clientId, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Client>)this).Get(projectionName, clientId, fetchMode);
		}

		public Client Get(Projection projection, Int32 clientId)
		{
			return ((IRepository<Client>)this).Get(projection, clientId, FetchMode.UseIdentityMap);
		}

		public Client Get(Projection projection, Int32 clientId, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Client>)this).Get(projection, clientId, fetchMode);
		}

		public Client Get(string projectionName, Int32 clientId, params string[] fields)
		{
			return ((IRepository<Client>)this).Get(projectionName, clientId, fields);
		}

		public Client Get(Projection projection, Int32 clientId, params string[] fields)
		{
			return ((IRepository<Client>)this).Get(projection, clientId, fields);
		}

		public bool Delete(Int32 clientId)
		{
			var entity = new Client { ClientId = clientId };
			return this.Delete(entity);
		}

				// asyncrhonous methods

		public System.Threading.Tasks.Task<Client> GetAsync(string projectionName, Int32 clientId)
		{
			return ((IRepository<Client>)this).GetAsync(projectionName, clientId, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<Client> GetAsync(string projectionName, Int32 clientId, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Client>)this).GetAsync(projectionName, clientId, fetchMode);
		}

		public System.Threading.Tasks.Task<Client> GetAsync(Projection projection, Int32 clientId)
		{
			return ((IRepository<Client>)this).GetAsync(projection, clientId, FetchMode.UseIdentityMap);
		}

		public System.Threading.Tasks.Task<Client> GetAsync(Projection projection, Int32 clientId, FetchMode fetchMode = FetchMode.UseIdentityMap)
		{
			return ((IRepository<Client>)this).GetAsync(projection, clientId, fetchMode);
		}

		public System.Threading.Tasks.Task<Client> GetAsync(string projectionName, Int32 clientId, params string[] fields)
		{
			return ((IRepository<Client>)this).GetAsync(projectionName, clientId, fields);
		}

		public System.Threading.Tasks.Task<Client> GetAsync(Projection projection, Int32 clientId, params string[] fields)
		{
			return ((IRepository<Client>)this).GetAsync(projection, clientId, fields);
		}

		public System.Threading.Tasks.Task<bool> DeleteAsync(Int32 clientId)
		{
			var entity = new Client { ClientId = clientId };
			return this.DeleteAsync(entity);
		}
		
		public void DeleteClientAndUserClient(Int32? clientId)
		{
            var executor = new StoredProcedureExecutor(this.DataService, true)
            {
                GetCommandFunc = () =>
                {
                    var proc =  Proyecto_Facturas.Data.StoredProcedures.CreateDeleteClientAndUserClientProcedure(this.DataService.Connection, this.DataService.EntityLiteProvider.ParameterPrefix, this.DataService.EntityLiteProvider.DefaultSchema);
					proc.Parameters[this.DataService.EntityLiteProvider.ParameterPrefix + "client_id"].Value = clientId == null ? (object) DBNull.Value : clientId.Value;
                    return proc;
                }
            };

			executor.ExecuteNonQuery();
		}

		public async System.Threading.Tasks.Task DeleteClientAndUserClientAsync(Int32? clientId)
		{
            var executor = new StoredProcedureExecutor(this.DataService, true)
            {
                GetCommandFunc = () =>
                {
                    var proc =  Proyecto_Facturas.Data.StoredProcedures.CreateDeleteClientAndUserClientProcedure(this.DataService.Connection, this.DataService.EntityLiteProvider.ParameterPrefix, this.DataService.EntityLiteProvider.DefaultSchema);
					proc.Parameters[this.DataService.EntityLiteProvider.ParameterPrefix + "client_id"].Value = clientId == null ? (object) DBNull.Value : clientId.Value;
                    return proc;
                }
            };

			await executor.ExecuteNonQueryAsync().ConfigureAwait(false);
		}
	}
	// [Obsolete("Use nameof instead")]
	public static partial class ClientFields
	{
		public const string ClientId = "ClientId";
		public const string CommercialName = "CommercialName";
		public const string LegalName = "LegalName";
		public const string Cif = "Cif";
		public const string Email = "Email";
		public const string Phone = "Phone";
		public const string Address = "Address";
		public const string CreationDate = "CreationDate";
	}

	public static partial class ClientProjections
	{
		public const string BaseTable = "BaseTable";
	}
	[Serializable]
	[DataContract]
	[SqlEntity(BaseTableName="User_Clients")]
	public partial class UserClients
	{
		private Int32 _userId;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, ColumnName ="user_id", BaseColumnName ="user_id", BaseTableName = "User_Clients" )]		
		public Int32 UserId 
		{ 
		    get { return _userId; } 
			set 
			{
			    _userId = value;
			}
        }

		private Int32 _clientId;
		[DataMember]
		[SqlField(DbType.Int32, 4, Precision = 10, IsKey=true, ColumnName ="client_id", BaseColumnName ="client_id", BaseTableName = "User_Clients" )]		
		public Int32 ClientId 
		{ 
		    get { return _clientId; } 
			set 
			{
			    _clientId = value;
			}
        }

		private DateTime _assignmentDate;
		[DataMember]
		[SqlField(DbType.DateTime, 8, Precision = 23, Scale=3, ColumnName ="assignment_date", BaseColumnName ="assignment_date", BaseTableName = "User_Clients" )]		
		public DateTime AssignmentDate 
		{ 
		    get { return _assignmentDate; } 
			set 
			{
			    _assignmentDate = value;
			}
        }


	}

	public partial class UserClientsRepository : Repository<UserClients> 
	{
		public UserClientsRepository(DataService DataService) : base(DataService)
		{
		}

		public new FacturacionDataService  DataService  
		{
			get { return (FacturacionDataService) base.DataService; }
			set { base.DataService = value; }
		}

	}
	// [Obsolete("Use nameof instead")]
	public static partial class UserClientsFields
	{
		public const string UserId = "UserId";
		public const string ClientId = "ClientId";
		public const string AssignmentDate = "AssignmentDate";
	}

	public static partial class UserClientsProjections
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

		private Proyecto_Facturas.Data.ProductRepository _ProductRepository;
		public Proyecto_Facturas.Data.ProductRepository ProductRepository
		{
			get 
			{
				if ( _ProductRepository == null)
				{
					_ProductRepository = new Proyecto_Facturas.Data.ProductRepository(this);
				}
				return _ProductRepository;
			}
		}

		private Proyecto_Facturas.Data.InvoiceStatusRepository _InvoiceStatusRepository;
		public Proyecto_Facturas.Data.InvoiceStatusRepository InvoiceStatusRepository
		{
			get 
			{
				if ( _InvoiceStatusRepository == null)
				{
					_InvoiceStatusRepository = new Proyecto_Facturas.Data.InvoiceStatusRepository(this);
				}
				return _InvoiceStatusRepository;
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

		private Proyecto_Facturas.Data.ClientRepository _ClientRepository;
		public Proyecto_Facturas.Data.ClientRepository ClientRepository
		{
			get 
			{
				if ( _ClientRepository == null)
				{
					_ClientRepository = new Proyecto_Facturas.Data.ClientRepository(this);
				}
				return _ClientRepository;
			}
		}

		private Proyecto_Facturas.Data.UserClientsRepository _UserClientsRepository;
		public Proyecto_Facturas.Data.UserClientsRepository UserClientsRepository
		{
			get 
			{
				if ( _UserClientsRepository == null)
				{
					_UserClientsRepository = new Proyecto_Facturas.Data.UserClientsRepository(this);
				}
				return _UserClientsRepository;
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

		public static DbCommand CreateDeleteUserWithInvoicesProcedure(DbConnection connection, string parameterPrefix, string schema = "")
		{
			var cmd = connection.CreateCommand();
			cmd.CommandText = string.IsNullOrEmpty(schema) ? "DeleteUserWithInvoices" : schema + "." + "DeleteUserWithInvoices";
			cmd.CommandType = CommandType.StoredProcedure;
			IDbDataParameter p = null;

			p = cmd.CreateParameter();
			p.ParameterName = parameterPrefix + "RETURN_VALUE";
			p.DbType = DbType.Int32;
            p.Direction = ParameterDirection.ReturnValue;
			p.SourceColumn = "RETURN_VALUE";
			cmd.Parameters.Add(p);

			p = cmd.CreateParameter();
			p.ParameterName = parameterPrefix + "user_id";
			p.DbType = DbType.Int32;
            p.Direction = ParameterDirection.Input;
			p.SourceColumn = "user_id";
			cmd.Parameters.Add(p);

			return cmd;
		}

		public static DbCommand CreateDeleteClientAndUserClientProcedure(DbConnection connection, string parameterPrefix, string schema = "")
		{
			var cmd = connection.CreateCommand();
			cmd.CommandText = string.IsNullOrEmpty(schema) ? "DeleteClientAndUserClient" : schema + "." + "DeleteClientAndUserClient";
			cmd.CommandType = CommandType.StoredProcedure;
			IDbDataParameter p = null;

			p = cmd.CreateParameter();
			p.ParameterName = parameterPrefix + "RETURN_VALUE";
			p.DbType = DbType.Int32;
            p.Direction = ParameterDirection.ReturnValue;
			p.SourceColumn = "RETURN_VALUE";
			cmd.Parameters.Add(p);

			p = cmd.CreateParameter();
			p.ParameterName = parameterPrefix + "client_id";
			p.DbType = DbType.Int32;
            p.Direction = ParameterDirection.Input;
			p.SourceColumn = "client_id";
			cmd.Parameters.Add(p);

			return cmd;
		}

	}
}
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor
			