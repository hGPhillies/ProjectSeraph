import axios, { type AxiosResponse } from "axios";
import type { BloodsugarMeasurement } from "../domain/BloodsugarMeasurement";

// Bem�rk: for at hente axios skal I taste disse to kommandoer i terminalvinduet

// (1) npm install axios
// (2) npm install --save-dev @types/react @types/react-dom

// REFACTOR
//Ret URL, så vi er sikre på at den matcher backend-endpoint
const BASE_URL = "http://localhost:8080/measurement/send/bloodsugar";

class BloodsugarService {
    static async sendMeasurement(measurement: BloodsugarMeasurement): Promise<void> {
        await axios
            .post<void, AxiosResponse<void>>(BASE_URL, measurement);
    }
}
export default BloodsugarService;