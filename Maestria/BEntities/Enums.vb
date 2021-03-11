Option Explicit On
Option Compare Text
Option Strict On
Option Infer On
'Public Enum ContactStatus
'    Registered = 11 'Registrado
'    Assigned = 12 'Asignado
'    Interested = 13 'Interesado
'    Discarded = 14 'Descartado
'    Accepted = 15 'Aceptado
'    Rejected = 16 'Rechazado
'End Enum

Public Enum NivelesPassword
    BASICO = 2482
    MEDIO = 2483
    FUERTE = 2484
    MUYFUERTE = 2485
End Enum
Public Enum Devolucion
    Devuelto = 1
    UsoComun = 0
End Enum
Public Enum UnidadNegocio
    Yotau = 1
    Paraguay = 2
End Enum

Public Enum ParametrosQueue
    Opc = 5000
    TeleOperador = 5000
    JefeMercadeo = 2000
End Enum

Public Enum TipoPagoCartera As Integer
    Prepagada = 1
    Pendiente = 2
    Importada = 3
End Enum

Public Enum EstadoLlamada
    TelefonoApagado = 1044 'Telefono Apagado
    LlamadaRechazada = 1045 'Llamada Rechazada
    TelefonoTimbra = 1046 'Telefono Timbra
    TelefonoNoActivo = 1047 'Telefono no activo
    TitularDeLaLinea = 1048 'Titular de la linea
    NumeroEquivocado = 1049 'Numero Equivocado
    NoVolverALlamar = 1050 ' No volver a llamar  
    NumeroReferido = 1051 ' contacto referido

End Enum

Public Enum EstadoReprogramacionLLamada
    Realizado = 1162
    Pendiente = 1163
    Deshabilitado = 1172

End Enum

Public Enum ParametersComissions
    SalesVolume = 1
    Sales30 = 2
    AdditionalSales50 = 3
    AdditionalSales100 = 4
    SalesUp5000 = 5
    SalesDown5000 = 6
    SalesQ = 7
    AdditionalxSales = 8
    Additional6QWeek = 9
    SalesVolume0To200 = 10
    SalesVolume201To300 = 11
    SalesVolumeUp301 = 12
    Adelantos = 13
End Enum
Public Enum ContactoCarteraContacto
    ContactoNoContestoLlamada = 1 'Cartera de contactos que no contestan y desasignados
    ContactoReferido = 2 'Cartera de contactos referidos
    'ContactReferred = 4637 'Cartera de contactos referidos
End Enum
'Public Enum ScheduleStatus
'    Scheduled = 5 'Programado
'    Executed = 6 'Ejecutado
'    Pending = 7 'Pendiente
'    Confirmed = 8 'Confirmado
'    Rescheduled = 9 'Reagendado
'    Discarded = 10 'Descartado
'End Enum
Public Enum EstadoContacto
    Registrado = 1064 'Registrado
    Asignado = 1065 'Asignado
    Interesado = 1066 'Interesado
    Descartado = 1067 'Descartado
    Aceptado = 1068 'Aceptado
    Rechazado = 1069 'Rechazado
    NumeroEquivocado = 2409 'Numero Equivocado
    NoExiste = 2410 'Numero no activo o no existe
    NoInteresado = 2411 'Contacto no interesado
    NoContesta = 2412 'Contacto no contesta, telefono apagado o lo cuelga
    NoPerfila = 2413 'Si no cumple con los requisitos aptos para venderle algun programa.
End Enum
Public Enum Estado
    Activo = 1 'Activo
    Inactivo = 0 'Inactivo o Eliminado
End Enum
Public Enum Monedas
    PesosChilenos = 1
    UF = 2
    Dolarestadounidense = 3
    PesosBolivianos = 4
End Enum
Public Enum EstadoCivil
    Soltero = 1057
    Casado = 1059
    viudo = 1061
    divorciado = 1062
End Enum
Public Enum EstadoSala
    Abierta = 1 'Abierta
    ReAbierta = 2 'Reaperturado
    Cerrada = 0 'Cerrada
End Enum
Public Enum EstadoCaja
    Abierta = 1093 'Abierta
    Cerrada = 1095 'Cerrada
    Rendida = 1110
    Cuadrada = 1111
    Reabierta = 1112
    Bloqueada = 1122
End Enum
Public Enum TypeStates
    Reserva = 84 'Reserva
    Ocupado = 85 'Ocupado
End Enum

Public Enum PaymentType
    Efectivo = 1
    Tarjeta = 2
    Cheque = 3
    DepositoBancario = 4
End Enum
Public Enum CardType
    Debito = 1
    Linser = 2
    ATC = 3
End Enum
Public Enum Temporada
    Alta = 1
    SuperAlta = 2
    Media = 3
    MediaAlta = 4
End Enum

Public Enum TipoAsignacion As Integer
    Manual = 1070
    Automatica = 1071
    ReAsignada = 1072
End Enum

Public Enum Proveedores As Integer
    GrupoSCI = 1
End Enum

'Public Enum RolsSystems As Long
'    Administradores = 2
'    Supervisor = 7
'    Teleoperador = 8
'    Closer = 11
'    Liner = 10
'    GerentedeVenta = 9
'    DirectorComercial = 12
'    Recepcion = 14
'    AtencionaSocios = 18
'    Digitador = 16
'    EncargadodeContratos = 15
'End Enum
Public Enum CargoEnSistema As Integer
    'Administradores = 1
    'GerenteAdministrativo = 2
    'Supervisor = 3
    'Teleoperador = 4
    'OPC = 5
    'JefeTMK = 6
    'cajero = 1007
    'JefeCobranza = 1010
    'cobrador = 1011
    'GerenteSala = 1012
    'Recepcion = 1013
    'VLO = 1014
    'Linner = 1015
    'Closer = 1016
    'Tesoreria = 1017
    'Contabilidad = 1018
    'GerenteComercial = 1019
    'Bizalia = 1020
    'GerenteNegocios = 1021
    'GerenteCobranzas = 1022
    'AtencionSocio = 1023
    'JefeMercadeo = 1024
    Administradores = 1
    GerenteAdministrativo = 2
    Supervisor = 0
    OPERADOR = 4
    OPC = 10
    GERENTEDEVENTA = 14
    DIRECTOR = 21
    JefeTMK = 0
    SUPERVISOROPC = 31
    JefeCobranza = 0
    COBRADOR = 12
    EJECUTIVA = 28
    CONTADOR = 23
    TESORERA = 24
    ASISTENTEMERCADEO = 25
    ASISTENTEADMINISTRATIVO = 18
    GERENTEMERCADEO = 22
    DIRECTORAGENERAL = 17
    FRONT = 20
    GARZON = 32
    GerenteSala = 5 'Jefe Sala 
    Recepcion = 15
    LINER = 7
    CLOSER = 6
    GerenteComercial = 3
    FACTURACION = 13
    GerenteNegocios = 2
    GerenteCobranzas = 11
    AtencionSocio = 11
    JefeMercadeo = 9
    GestionSala = 8
    JefaComercial = 30
    CoordinadorOPC = 33
End Enum

Public Class mapas

    Dim mapa As New Hashtable()

End Class


Public Enum Nacionalities As Long
    Bolivian = 34
End Enum

Public Enum ContractPercent As Long
    Initial = 86
    Regular = 87
End Enum
Public Enum DatabasePaymentType As Long
    Prepaid = 2 'Prepagada
    PerContract = 3 'Por Contrato
End Enum

Public Enum CityBusines
    SantaCruz = 57 'Ciudad de Santa Cuz
    Cochabamba = 63 'Ciudad de Cochabamba
    LaPaz = 64 'Ciudad de La Paz
    ElAlto = 65 'Ciudad de El Alto
    Sucre = 66 'Ciudad de Sucre
    Montero = 67 'Ciudad de El Alto
    Beni = 68 'Ciudad de Sucre
    Peru = 69 'Ciudad de El Alto
    Quillacollo = 70 'Ciudad de Sucre
    General = 0 'Referencia para cualquier ciudad
End Enum

Public Enum ContractPaymentMode
    CashPayment = 28 ' Pago en efectivo
    QuotaPayment = 29 ' Pago en cuotas
End Enum

Public Enum ClasificacionUPS
    Notour = 23
    NoShow = 24
    NoCalifica = 1007
    Califica = 1006
End Enum

Public Enum MeetingStatusType
    Sale = 25
    NoSale = 26
End Enum

Public Enum QuotaPlanStatus
    Pending = 42
    Paid = 43
End Enum

Public Enum ContractStatus
    Active = 40
    Dismiss = 41
End Enum

Public Enum FeeStatus
    Pending = 44
    Paid = 45
End Enum

Public Enum QuotasType
    Administrative = 54
    Regular = 55
End Enum

Public Enum FeeType
    Percentage = 61
    FixedAmount = 62
End Enum

Public Enum ProviderPaymentStatus
    Active = 63
End Enum

Public Enum loginStatus
    Login = 81
    Logout = 82
    Loginfailed = 83
End Enum

Public Enum TipoClasificadores
    AsuntoCircular = 1086
    EstadoContacto = 1
    EstadoAsignacion = 2
    EstadoTipoPrograma = 1062
    CarteraTipoPago = 3
    Categoria = 5
    TipoTransaccion = 6
    ConceptoCajaIngreso = 8
    TipoPagoCajaIngreso = 9
    BancoCajaIngreso = 10
    TipoTarjetaCajaIngreso = 11
    FormaPagoContrato = 1039
    TipoMandato = 1030
    Nacionalidad = 1031
    EstadoCivil = 1040
    Banco = 1032
    Concepto = 1034
    ConceptoMulta = 1083
    TipoCuenta = 1035
    EstadoMandato = 1037
    EstadoLlamada = 1038
    EstadoReprogramacionLLamada = 1067
    EstadosCuotas = 1044
    TiposCuotas = 1045
    EstadoCargos = 1046
    EstadoHotel = 12
    OrigenUps = 1028
    CategoriaServicio = 1052
    TipoSolicitud = 1054
    TipoEstadoContrato = 1047
    EstadoComision = 1057
    TipoEgreso = 1058
    EstadoBeneficiario = 1060
    EstadoCajaIngreso = 1061
    Convertidor = 1029
    ClasificacionUps = 1027
    Trabajo = 1041
    TarjetaEnjoy = 1081
    EstadoCheque = 1063
    EstadoPuntoControl = 1064
    EstadoCuotaMantfencion = 1065
    EstadoTipoDocumento = 1066
    TipoCartaCobranza = 1068
    TipoSeguimiento = 1051
    Sexo = 1074
    TipoEnvioWebService = 1075
    TipoWebService = 1076
    EstadoMoneda = 1069
    EstadoTipoCambio = 1070
    DescripcionMonto = 1173
    Periodicidad = 1174
    Nomenclatura = 1071
    EstadoValorPunto = 1077
    Descriptor = 1072
    PrioridadHelpDesk = 1078
    EstadoUsoPuntos = 1082
    TipoTicket = 1084
    EstadoHelpDesk = 1085
    TipoTransaccionBitacora = 1089
    DependenciaPersonal = 1091
    CategoriaContacto = 1092
    TipoContrato = 1093
    EstadoParametroPuntosAdicionales = 1088
    EstadoControlPuntosAdicionales = 1094
    EstadoBoletaElectronica = 1095
    EstadoTransaccionBoletaElectronica = 1096
    NivelesPassword = 1097
    MarcaVehiculo = 1098
    ModeloVehiculo = 1099
    TipoVehiculo = 1100
    OrigenVehiculo = 1101
    Expedido = 1102
    ParametroOrigen = 1103
    ParametroDescuento = 1104
    EstadoCotizacion = 1105
    EstadoPoliza = 1106
End Enum

Public Enum EstadoCotizacion
    PENDIENTE = 2530
    COMPLETADA = 2531
    ANULADA = 2532
    VENCIDA = 2533
End Enum
Public Enum DescuentoParametro As Integer
    MaximoDescuentoCotizacion = 2526
End Enum
Public Enum ParametroOrigen As Integer
    China = 2527
End Enum
Public Enum TiposCarteraContactos As Integer
    PPDD = 2484
    Referido = 2485
End Enum
Public Enum UbicacionLotes
    Pavimento = 2480
    Esquina = 2481
    Avenida = 2482
    Calle = 2483
End Enum

Public Enum EstadoTransaccionBoletaElectronica
    Existosa = 2478
    Fallida = 2479
End Enum
Public Enum EstadoBoletaElectronica
    SinGenerar = 2474
    Generada = 2475
    Cancelada = 2476
    Consolidada = 2477
    Manual = 2480
End Enum
Public Enum EstadoControlPuntosAdicionales
    Solicitado = 2470
    Activado = 2471
    Anulado = 2472
End Enum
Public Enum TipoContrato
    ContratoNormal = 2425
    ContratoExit = 2426
End Enum
Public Enum SexoContacto
    Masculino = 1185
    Femenino = 1086
End Enum
Public Enum TipoTransaccionBitacora
    Exitosa = 1226
    Fallida = 1227
End Enum
Public Enum TipoTicket
    Funcionalidad = 1216
    Falla = 1217
End Enum
Public Enum EstadoHelpDesk
    Abierto = 1218
    Cerrado = 1219
    TiposMembresia = 1079
    EstadosMembresias = 1080
End Enum
Public Enum TiposMembresia
    MembresíaIntervalBásica = 1198
    MembresíaIntervalGold = 1199
End Enum
Public Enum EstadosMembresias
    Vigente = 1200
    Vencidas = 1201
End Enum
Public Enum PrioridadHelpDesk
    Alta = 1195
    Media = 1196
    Baja = 1197
End Enum
Public Enum EstadoUsoPuntos
    Reservado = 1204
    Utilizado = 1205
    ColaEspera = 1206
End Enum

Public Enum Descriptor
    Descripcion = 1173
    Periodicidad = 1174
End Enum

Public Enum EstadoValorPunto
    Vigente = 1193
    Vencido = 1194
End Enum
Public Enum ModulosSistema
    Mantencion = 3
    Administracion = 4
    Telemarketing = 5
    Ventas = 6
    Cobranza = 8
    Seguridad = 9
    Reportes = 10
    Comisiones = 11
    Inventario = 12
    Caja = 13
    Contrato = 15
    Tesoreria = 1016
    InventarioDeUso = 1017
End Enum
Public Enum TipoWebService
    ValidarCliente = 1190
    RegistroPuntos = 1191
    'ValidarClienteContrato = 1245
    'para la 22
    ValidarClienteContrato = 1230
End Enum
Public Enum DescripcionMonto
    MontoFijo = 1
    MontoFijoUFV = 2
    MontoVariable = 3
    Otro = 4
End Enum
Public Enum TipoEnvioWebService
    EnviadoCorrectamente = 1188
    EnvioFallido = 1189
End Enum

Public Enum Periodicidad
    MensualIndefinida = 5
    MensualHasta = 6
    MeseAContar = 7
    PorUnaSolaVez = 8
    IndefinidaADeterminar = 9
    Otro = 10
End Enum
Public Enum Nomenclatura
    PreDescripcion = 1175
    Descripcion = 1176
    PostDescripcion = 1177
    PrePostDescripcion = 1178
    InDescripcion = 1179
End Enum
Public Enum EstadoTipoDocumento
    Contratos = 1158
    OtrosAdjuntos = 1159
End Enum



Public Enum TipoCartaCobranza
    Cobranzas = 1164
    Bienvenida = 1165
    Llamada = 1167
End Enum
Public Enum ServiciosEnjoy
    FEE = 9
End Enum
Public Enum ServiciosWebService
    TraspasoPuntos = 1181
End Enum
Public Enum EstadoCuotaMantencion
    Pagada = 1156
    NoPagada = 1157
End Enum
Public Enum EstadoParametroPuntosAdicionales
    Habilitado = 1223
    Deshabilitado = 1224
    Anulado = 2469
End Enum

Public Enum EstadoPuntoControl
    Vigente = 1152
    Finalizado = 1153
    Vencido = 1154
End Enum

Public Enum EstadoCheque
    Habilitado = 1140
    Desabilitado = 1141
    Cobrado = 1150
    Descartado = 1151
End Enum

Public Enum Categoria
    EnjoyVacations = 4
    EnjoyVacationsPlus = 5
End Enum


Public Enum EstadoCajaIngreso
    Activo = 1128
    Inactivo = 1129
End Enum

Public Enum FormaPagoContrato
    Efectivo = 1053
    Credito = 1054
    TarjetaDebito = 1322
    Cheque = 1136
    CuotasPAC = 1137
    CuotasPAT = 1138
    Transferencia = 1139
End Enum

Public Enum EstadoComision
    Pagado = 1108
    Pendiente = 1107
End Enum

Public Enum EstadoBeneficiario
    Habilitado = 1120
    Inabilitado = 1121
End Enum
Public Enum CategoriaServicio
    Hotel = 1099
    Evento = 1100
    Habitacion = 1101
    FEE = 1161
    Interval = 1301
End Enum

Public Enum TipoEgreso
    Anulacion = 1116
    Devolucion = 1117
End Enum

Public Enum EstadoAsignacion
    Programado = 1035
    Ejecutado = 1036
    Pendiente = 1037
    Confirmada = 1039
    Reagendado = 1040
    Descartado = 1041
End Enum
Public Enum EstadoTipoPrograma
    Activo = 1134
    Inactivo = 1135
End Enum

Public Enum EstadoSolicitud
    Pendiente = 1104
    Aprobado = 1105
    Denegado = 1106
End Enum

Public Enum EstadoContrato
    Emitido = 1089
    Inactivo = 1090
    Verificado = 1123
    Vigente = 1124
    AnuladoCDevolucion = 1125
    AnuladoSDevolucion = 1126
    NoVigente = 1142
    Finalizado = 1149
    Reserva = 2456
    ReservaEmision = 2457
    ReservaDevolucion = 2458
End Enum

Public Enum EstadoConfirmacion
    Confirmada = 1
    NoConfirmada = 0
End Enum

Public Enum TipoTransaccion
    Ingreso = 6
    Egreso = 7
    Inventario = 8
    AnulacionUsoPuntos = 1207
End Enum
Public Enum TipoMandato
    PAC = 1014
    PAT = 1015
End Enum
Public Enum Nacionalidad
    Boliviana = 1016
    Brasilera = 1017
    Argentina = 1018
    Paraguaya = 1019
    Uruguaya = 1020
    Chilena = 1021
    Peruana = 1022
    Venezolana = 1023
    Colombiana = 1024
    Ecuatoriana = 1025
End Enum

Public Enum Banco
    BancoChile = 1026
    BancoBCI = 1027
    BancoSantander = 1028
End Enum

Public Enum Concepto
    Inicial = 1029
    Total = 1030
    Mantenimiento = 1031
End Enum
Public Enum ConceptoMulta
    DaniosPropiedad = 1208
    CheckOutFueraPlazo = 1009
    InfraccionReglamentoDeHotel = 1010
    NoPagoDeExtras = 1015
End Enum
Public Enum TipoCuenta
    CuentaCorriente = 1032
    CuentaAhorro = 1033
    TarjetaBancaria = 1034
End Enum

Public Enum EstadoMandato
    Habilitado = 1042
    Deshabilitado = 1043
End Enum

Public Enum EstadosCuotas
    Vigente = 1073
    Pagada = 1074
    Mora = 1076
    PorConciliar = 1077
    Egresado = 1155

    UltimosPagados = 1113
    Todos = 1115
    MoraHistorial = 1118
    Devolucion = 1130
    Eliminada = 1131
End Enum

Public Enum TiposCuotas
    Inicial = 1078
    CapitalTotal = 1080
    Mantenimiento = 1081
    Multas = 1213
    RenovacionMembresia = 1214
    Refinanciamiento = 1212
    PagoPuntosAdicionales = 1211
    PuntosAdicionales = 2473
End Enum

Public Enum TipoPagoCuotasContrato
    Contado = 1053
    Credito = 1054
End Enum

Public Enum TrabajoClienteContacto
    Contador = 1063
    Doctor = 1091
    Jugador = 1092
End Enum

Public Enum EstadoCargos
    Generado = 1082
    Emitido = 1083
    Procesado = 1084
    Descartado = 1085
    ConciliadoCorrecto = 1086
    ConciliadoIncorrecto = 1087
End Enum

Public Enum EstadoHotel
    Habilitado = 21
    Deshabilitado = 22
End Enum

Public Enum OrigenUps
    OperadorTeleMarketing = 1009
    OperadorPrimerContacto = 1010
    OperadorWeb = 1184
    OperadorWalkin = 1182
    OperadorReferido = 1183
End Enum
Public Enum TipoSeguimiento
    Llamada = 1098
    Carta = 1096
End Enum
Public Enum EstMoneda
    Habilitado = 1168
    Desabilitado = 1169
End Enum
Public Enum TipoCambio
    Definido = 1170
    NoDefinido = 1171
    Vencido = 1192
End Enum
Public Enum EstadoTArjetaEnjoy
    Vigente = 1202
    Vencida = 1203
End Enum
Public Enum AsuntoCircular
    NoticiaDeHotal = 1020
    CanjeDePuntos = 1021
    NuevoProducto = 1222
End Enum
Public Class URLFormularios

    Private Key As String

    Public Shared ReadOnly Personal As URLFormularios = New URLFormularios("/Modulos/Seguridad/Personal.aspx")
    Public Shared ReadOnly PermisoaPersonales As URLFormularios = New URLFormularios("/Modulos/Seguridad/PermisoPersonal.aspx")
    Public Shared ReadOnly Proveedor As URLFormularios = New URLFormularios("/Modulos/Captacion/Proveedor.aspx")
    Public Shared ReadOnly CarteradeContacto As URLFormularios = New URLFormularios("/Modulos/Captacion/CarteraContacto.aspx")
    Public Shared ReadOnly Contacto As URLFormularios = New URLFormularios("/Modulos/Captacion/Contacto.aspx")
    Public Shared ReadOnly TipoClasificadores As URLFormularios = New URLFormularios("/Modulos/Base/ClasificadoresTipo.aspx")
    Public Shared ReadOnly CargosyJerarquias As URLFormularios = New URLFormularios("/Modulos/Base/CargoJerarquia.aspx")
    Public Shared ReadOnly Temporadas As URLFormularios = New URLFormularios("/Modulos/Inventario/Temporada.aspx")
    Public Shared ReadOnly TiposdeprogramasyValores As URLFormularios = New URLFormularios("/Modulos/Inventario/TipoPrograma.aspx")
    Public Shared ReadOnly DefinicióndeSala As URLFormularios = New URLFormularios("/Modulos/Venta/Sala.aspx")
    Public Shared ReadOnly DefiniciondeCaja As URLFormularios = New URLFormularios("/Modulos/Caja/Caja.aspx")
    Public Shared ReadOnly AperturayCierredeSala As URLFormularios = New URLFormularios("/Modulos/Venta/InstanciaSala.aspx")
    Public Shared ReadOnly Periodossemanales As URLFormularios = New URLFormularios("/Modulos/Inventario/PeriodoSemanal.aspx")
    Public Shared ReadOnly HistorialCaja As URLFormularios = New URLFormularios("/Modulos/Caja/CajaInstancia.aspx")
    Public Shared ReadOnly ConfiguracionHoteles As URLFormularios = New URLFormularios("/Modulos/Inventario/HotelPrograma.aspx")
    Public Shared ReadOnly MovimientodeSala As URLFormularios = New URLFormularios("/Modulos/Venta/MovimientoSala.aspx")
    'Public Shared ReadOnly IngresoaCaja As URLFormularios = New URLFormularios("/Modulos/Caja/CajaIngreso.aspx")
    Public Shared ReadOnly ConsultayEmision As URLFormularios = New URLFormularios("/Modulos/Contrato/Contrato.aspx")
    Public Shared ReadOnly Clasificadores As URLFormularios = New URLFormularios("/Modulos/Base/Clasificadores.aspx")
    Public Shared ReadOnly ConfirmaciondeCita As URLFormularios = New URLFormularios("/Modulos/CallCenter/Appointment.aspx")
    Public Shared ReadOnly AsignacionManual As URLFormularios = New URLFormularios("/Modulos/CallCenter/AsignacionManual.aspx")
    Public Shared ReadOnly LlamadaaContactos As URLFormularios = New URLFormularios("/Modulos/CallCenter/Llamadas.aspx")
    Public Shared ReadOnly AsignaciondeCartera As URLFormularios = New URLFormularios("/Modulos/Cobranza/AsignarCobrador.aspx")
    Public Shared ReadOnly GestionMandatos As URLFormularios = New URLFormularios("/Modulos/Tesoreria/Mandato.aspx")
    Public Shared ReadOnly GeneraciondeCargos As URLFormularios = New URLFormularios("/Modulos/Tesoreria/Cargo.aspx")
    Public Shared ReadOnly AtenciondeCartera As URLFormularios = New URLFormularios("/Modulos/Cobranza/Cobrador.aspx")
    Public Shared ReadOnly AsignaciondeContactos As URLFormularios = New URLFormularios("/Modulos/CallCenter/AsignacionContactos.aspx")
    Public Shared ReadOnly ImportaciondeContactos As URLFormularios = New URLFormularios("/Modulos/CallCenter/ImportacionContacto.aspx")
    Public Shared ReadOnly GestiondePuntos As URLFormularios = New URLFormularios("/Modulos/AtencionAlSocio/Clientes.aspx")
    Public Shared ReadOnly ParametrosCobranza As URLFormularios = New URLFormularios("/Modulos/Cobranza/ParametroCobranza.aspx")
    Public Shared ReadOnly Servicios As URLFormularios = New URLFormularios("/Modulos/Base/Servicios.aspx")
    Public Shared ReadOnly SolicitudesdeCajaEnConstruccion As URLFormularios = New URLFormularios("/Modulos/Caja/CajaSolicitud.aspx")
    Public Shared ReadOnly RespuestaaSolicitudesEnConstruccion As URLFormularios = New URLFormularios("/Modulos/Caja/Solicitudes.aspx")
    Public Shared ReadOnly ParametrosVenta As URLFormularios = New URLFormularios("/Modulos/Venta/ParametroCargo.aspx")
    Public Shared ReadOnly PermisoaFormularios As URLFormularios = New URLFormularios("/Modulos/Seguridad/PermisoFormulario.aspx")
    Public Shared ReadOnly ReasignaciondeCobrador As URLFormularios = New URLFormularios("/Modulos/Cobranza/Reasignacion.aspx")
    Public Shared ReadOnly CargosHistoricos As URLFormularios = New URLFormularios("/Modulos/Tesoreria/HistoricoCargos.aspx")
    Public Shared ReadOnly ControlReapertura As URLFormularios = New URLFormularios("/Modulos/Caja/ReingresoCaja.aspx")
    Public Shared ReadOnly ControlCaja As URLFormularios = New URLFormularios("/Modulos/Tesoreria/Rendimiento.aspx")
    Public Shared ReadOnly Contablidad As URLFormularios = New URLFormularios("/Modulos/Reportes/Contabilidad.aspx")
    Public Shared ReadOnly Verificacion As URLFormularios = New URLFormularios("/Modulos/Contrato/Verificacion.aspx")
    Public Shared ReadOnly Validacion As URLFormularios = New URLFormularios("/Modulos/Contrato/Validacion.aspx")
    Public Shared ReadOnly Cobranzas As URLFormularios = New URLFormularios("/Modulos/Reportes/Cobranzas.aspx")
    Public Shared ReadOnly GeneraciondeComisiones As URLFormularios = New URLFormularios("/Modulos/Comision/Comision.aspx")
    Public Shared ReadOnly Tesoreria As URLFormularios = New URLFormularios("/Modulos/Reportes/Tesoreria.aspx")
    Public Shared ReadOnly Inventario As URLFormularios = New URLFormularios("/Modulos/Reportes/Inventario.aspx")
    Public Shared ReadOnly Ventas As URLFormularios = New URLFormularios("/Modulos/Reportes/Ventas.aspx")
    Public Shared ReadOnly Refinanciamiento As URLFormularios = New URLFormularios("/Modulos/Contrato/Refinanciamiento.aspx")
    Public Shared ReadOnly ReaperturadeSala As URLFormularios = New URLFormularios("/Modulos/Venta/ReaperturaSala.aspx")
    Public Shared ReadOnly DocumentosdeContrato As URLFormularios = New URLFormularios("/Modulos/Contrato/DoumentoContrato.aspx")
    Public Shared ReadOnly GestionCuentas As URLFormularios = New URLFormularios("/Modulos/Tesoreria/GestionCuentas.aspx")
    Public Shared ReadOnly PalabrasClavesdelContrato As URLFormularios = New URLFormularios("/Modulos/Contrato/PalabrasClaveContrato.aspx")
    Public Shared ReadOnly Moneda As URLFormularios = New URLFormularios("/Modulos/Venta/Moneda.aspx")
    Public Shared ReadOnly TipodeCambio As URLFormularios = New URLFormularios("/Modulos/Venta/TipoCambio.aspx")
    Public Shared ReadOnly PalabrasClavesDeCartaCobranza As URLFormularios = New URLFormularios("/Modulos/Cobranza/PalabraClaveCobranza.aspx")
    Public Shared ReadOnly ValorPuntos As URLFormularios = New URLFormularios("/Modulos/Venta/ValorPuntos.aspx")
    Public Shared ReadOnly Mantencion As URLFormularios = New URLFormularios("/Modulos/Contrato/ParametroMantencion.aspx")
    Public Shared ReadOnly RegistrodeInsidencias As URLFormularios = New URLFormularios("/Modulos/HelpDesk/HelpDesk.aspx")
    Public Shared ReadOnly TarjetaHotel As URLFormularios = New URLFormularios("/Modulos/AtencionAlSocio/TarjetaEnjoy.aspx")
    Public Shared ReadOnly Multas As URLFormularios = New URLFormularios("/Modulos/AtencionAlSocio/DetalleMulta.aspx")
    Public Shared ReadOnly PersonalDepartamento As URLFormularios = New URLFormularios("/Modulos/Seguridad/PersonalDepartamento.aspx")
    Public Shared ReadOnly MembresiaInterval As URLFormularios = New URLFormularios("/Modulos/Contrato/RenovacionMembresia.aspx")
    Public Shared ReadOnly Circular As URLFormularios = New URLFormularios("/Modulos/Seguridad/Circular.aspx")
    Public Shared ReadOnly Finalizaciondecontrato As URLFormularios = New URLFormularios("/Modulos/Contrato/Finalizacion.aspx")
    Public Shared ReadOnly Departamentos As URLFormularios = New URLFormularios("/Modulos/Base/Departamento.aspx")
    Public Shared ReadOnly Devolucion As URLFormularios = New URLFormularios("/Modulos/Contrato/ParametroDevolucion.aspx")
    Public Shared ReadOnly Personaldesala As URLFormularios = New URLFormularios("/Modulos/Venta/PersonalSala.aspx")
    Public Shared ReadOnly InteresesyDescuentos As URLFormularios = New URLFormularios("/Modulos/Contrato/ParametroDescuento.aspx")
    Public Shared ReadOnly ParametroClub As URLFormularios = New URLFormularios("/Modulos/AtencionAlSocio/ParametroEnjoyClub.aspx")
    Public Shared ReadOnly IngresoaCaja As URLFormularios = New URLFormularios("/Modulos/Caja/CajaIngresoV2.aspx")
    Public Shared ReadOnly CompararVentas As URLFormularios = New URLFormularios("/Modulos/Reportes/MovimientoSalaQ.aspx")
    Public Shared ReadOnly SaladeTMK As URLFormularios = New URLFormularios("/Modulos/CallCenter/SalaTMK.aspx")
    Public Shared ReadOnly Grupodesala As URLFormularios = New URLFormularios("/Modulos/CallCenter/GrupoSalaTMK.aspx")
    Public Shared ReadOnly AsignaciondeOperador As URLFormularios = New URLFormularios("/Modulos/CallCenter/AsignacionOperador.aspx")
    Public Shared ReadOnly InventarioUso As URLFormularios = New URLFormularios("/Modulos/Reportes/InventarioUso.aspx")
    Public Shared ReadOnly Mora As URLFormularios = New URLFormularios("/Modulos/Contrato/ParametroMora.aspx")
    Public Shared ReadOnly ParametroPorcentajeComision As URLFormularios = New URLFormularios("/Modulos/Comision/ParametroPorcentaje.aspx")
    Public Shared ReadOnly Documentocobranza As URLFormularios = New URLFormularios("/Modulos/Cobranza/DocumentoCobranza.aspx")
    Public Shared ReadOnly Asignarnumerodeboleta As URLFormularios = New URLFormularios("/Modulos/Tesoreria/BoletaIngresoSinNumero.aspx")
    Public Shared ReadOnly Teleoperadores As URLFormularios = New URLFormularios("/Modulos/Reportes/Teleoperadores.aspx")
    Public Shared ReadOnly PermisoPlantilla As URLFormularios = New URLFormularios("/Modulos/Seguridad/PermisoPlantilla.aspx")
    Public Shared ReadOnly ProgramaporSaladeVenta As URLFormularios = New URLFormularios("/Modulos/Inventario/ProgramaSalaVenta.aspx")
    Public Shared ReadOnly ContratoReserva As URLFormularios = New URLFormularios("/Modulos/Contrato/ContratoReserva.aspx")
    Public Shared ReadOnly AtencionaSocio As URLFormularios = New URLFormularios("/Modulos/Reportes/AtencionSocio.aspx")
    Public Shared ReadOnly CambiodePrograma As URLFormularios = New URLFormularios("/Modulos/Contrato/CambioPrograma.aspx")
    Public Shared ReadOnly BolsadePuntos As URLFormularios = New URLFormularios("/Modulos/Base/BolsaPunto.aspx")
    Public Shared ReadOnly ParametroPuntoAdicional As URLFormularios = New URLFormularios("/Modulos/Base/ParametroPuntoAdicional.aspx")
    Public Shared ReadOnly PuntosAdicionales As URLFormularios = New URLFormularios("/Modulos/AtencionAlSocio/PuntosAdicionales.aspx")
    Public Shared ReadOnly BoletaElectronica As URLFormularios = New URLFormularios("/Modulos/Caja/BoletaElectronica.aspx")
    Public Shared ReadOnly ImportaciondePersonal As URLFormularios = New URLFormularios("/Modulos/CallCenter/ImportacionPersonal.aspx")
    Public Shared ReadOnly Vehiculo As URLFormularios = New URLFormularios("/Modulos/Aseguradora/Vehiculo.aspx")
    Public Shared ReadOnly Cliente As URLFormularios = New URLFormularios("/Modulos/Aseguradora/Cliente.aspx")
    Public Shared ReadOnly Cotizacion As URLFormularios = New URLFormularios("/Modulos/Aseguradora/Cotizacion.aspx")
    Public Shared ReadOnly UnidadNegocio As URLFormularios = New URLFormularios("/Modulos/Base/UnidadNegocio.aspx")
    Public Shared ReadOnly Albunes As URLFormularios = New URLFormularios("/Modulos/Albunes/Albunes.aspx")
    Public Shared ReadOnly Poliza As URLFormularios = New URLFormularios("/Modulos/Aseguradora/Poliza.aspx")

    Private Sub New(key As String)
        Me.Key = key
    End Sub

    Public Overrides Function ToString() As String
        Return Me.Key
    End Function
End Class
Public Enum FormularioDeModulos
    Personal = 1
    PermisoAPersonales = 2
    Proveedor = 4
    CarteraDeContacto = 5
    Contacto = 6
    TipoClasificadores = 7
    CargosYJerarquias = 1008
    Temporadas = 2008
    TiposDeProgramasYValores = 2009
    DefinicionDeSala = 2010
    DefinicionDeCaja = 2011
    AperturaYCierreDeSala = 2012
    PeriodosSemanales = 2013
    HistorialCaja = 2015
    ConfiguracionHoteles = 2016
    MovimientoDeSala = 2017
    IngresoACaja = 2018
    ConsultaYEmision = 2021
    Clasificadores = 3019
    ConfirmacionDeCita = 3021
    AsignacionManual = 3022
    LlamadaAContactos = 3023
    AsignacionDeCartera = 3027
    GestionMandatos = 3028
    GeneracionDeCargos = 3029
    AtencionDeCartera = 3031
    AsignacionDeContactos = 3032
    ImportacionDeContactos = 3034
    GestionDePuntos = 3035
    ParametrosCobranza = 3037
    Servicios = 3038
    ParametrosVenta = 3042
    PermisoAFormularios = 3043
    ReasignacionDeCobrador = 3050
    CargosHistoricos = 3051
    ControlCaja = 3053
    Contabilidad = 3056
    Verificacion = 3059
    Validacion = 3060
    Cobranzas = 3061
    GeneracionDeComisiones = 3062
    Tesoreria = 3063
    Inventario = 3064
    ventas = 3065
    Refinanciamiento = 3067
    ReaperturaDeSala = 3068
    DocumentosDeContrato = 3069
    GestionDeCuentas = 3070
    PalabrasClaveDelContrato = 3071
    Moneda = 3072
    TipoDeCambio = 3074
    PalabrasClaveDeCartaCobranza = 3075
    ValorPuntos = 3076
    Mantencion = 3077
    RegistroDeInsidencias = 3078
    TarjetaEnjoy = 3082
    Multas = 3083
    PersonalDepertamento = 3084
    MembresiaInterval = 3085
    Circular = 3086
    FinalizacionDeContrato = 3087
    Departamento = 3088
    ParametroDevolucion = 3089
    PersonalDeSala = 3090
    ParametroDescuento = 3091
    ParametroEnjoyClub = 3092
    IngresoCajaV2 = 3093
    CompararVentas = 3094
    SalaDeTelemaketing = 3095
    GrupoDeSala = 3096
    AsignacionDeOperador = 3097
    InventarioUso = 3098
    Mora = 3100
    ParametroPorcentajeComision = 3101
    DocumentoCobranza = 3102
    AsignarNumeroDeBoleta = 3103
    Teleoperadores = 3104
    PermisoPlanilla = 3106
    ProgramaSalaVenta = 3107
    ContratoReserva = 3108
    ReporteAtencionSocio = 3109
    CambioPrograma = 3110
    BolsaPunto = 3111
    ParametroPuntoAdicional = 3112
    ControlPuntoAdicional = 3113
    BoletaElectronica = 3114
    ImportacionDeContactosEncuestadores = 3114
    Vehiculo = 4208
End Enum

Public Enum DependenciaPersonal
    'EnjoyVacactions = 1228
    'BisaliaLatam = 1229
    Yotau = 1299
    BisaliaLatam = 1300
End Enum

