import { FechaCantidadValor, IFechaCantidadValor } from "./fechaCantidadValor";
import { IRecaudosPorEstacion, RecaudosPorEstacion } from "./RecaudosPorEstacion";

export interface IRecaudosPorFechaYEstacion{
    estacion: string;
    fechaCantidadValor: IFechaCantidadValor[];
    recaudosEstacion : IRecaudosPorEstacion;
}

export class RecaudosPorFechaYEstacion implements IRecaudosPorFechaYEstacion{
    estacion: string;
    fechaCantidadValor: FechaCantidadValor[] = [];
    recaudosEstacion : RecaudosPorEstacion;
}