import { IRecaudosPorFechaYEstacion, RecaudosPorFechaYEstacion } from "./recaudosPorFechaYEstacion";

export interface IDataReporte{
    dataRecaudosFechaEstacion : IRecaudosPorFechaYEstacion[];
    totalCantidad: string;
    totalValor: string;
}

export class DataReporte implements IDataReporte{
    dataRecaudosFechaEstacion : RecaudosPorFechaYEstacion[] = [];
    totalCantidad: string;
    totalValor: string;
}