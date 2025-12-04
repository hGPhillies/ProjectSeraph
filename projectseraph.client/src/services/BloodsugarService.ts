import axios, { type AxiosResponse } from "axios";
import type { BloodsugarMeasurement } from "../domain/BloodsugarMeasurement";

// REFACTOR
//Ret URL, så vi er sikre på at den matcher backend-endpoint
const BASE_URL = "https://localhost:5001/measurement/send/bloodsugar";

class BloodsugarService {
    static async sendMeasurement(measurement: BloodsugarMeasurement): Promise<void> {
        await axios
            .post<void, AxiosResponse<void>>(BASE_URL, measurement);
    }
}
export default BloodsugarService;