import axios, { type AxiosResponse } from "axios";
import type { BloodpressureMeasurement } from "../domain/BloodpressureMeasurement"; 

// REFACTOR
//VÆLG ÉN af disse(ofte er det https - versionen der matcher Swagger):
// const BASE_URL = "https://localhost:5001";
// const BASE_URL = "http://localhost:5000";

//Ret URL, så vi er sikre på at den matcher backend
const BASE_URL = "http://localhost:5001/api/bloodpressure";

class BloodpressureService
{
    static async sendMeasurement(measurement: BloodpressureMeasurement): Promise<void>
    {
        await axios
            .post<void, AxiosResponse<void>>(BASE_URL, measurement); 
    }
}
export default BloodpressureService;