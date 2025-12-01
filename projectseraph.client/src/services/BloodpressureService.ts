import axios, { type AxiosResponse } from "axios";
import type { BloodpressureMeasurement } from "../domain/BloodpressureMeasurement"; 

// REFACTOR
//Ret URL, så vi er sikre på at den matcher backend-endpoint
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