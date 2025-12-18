import axios, { type AxiosResponse } from "axios";
import type { BloodpressureMeasurement } from "../domain/BloodpressureMeasurement";

// REFACTORq
//Ret URL, så vi er sikre på at den matcher backend-endpoint
const BASE_URL = "http://localhost:8080/measurement/send/bloodpressure";

class BloodpressureService {
    static async sendMeasurement(measurement: BloodpressureMeasurement): Promise<void> {
        await axios
            .post<void, AxiosResponse<void>>(BASE_URL, measurement);
    }
}
export default BloodpressureService;