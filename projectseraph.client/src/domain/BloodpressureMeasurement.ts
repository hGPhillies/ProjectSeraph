// TypeScript interface representing a blood pressure measurement sent from the React client.
// This defines the exact shape of the JSON object submitted to the backend.
// All three fields are required and must be numbers.
export interface BloodpressureMeasurement
{
    systolic: number; //Systolic blood pressure in mmHg
    diastolic: number; //Diastolic blood pressure in mmHg
    pulse: number; //Pulse rate in beats per minute
    time: string; //ISO 8601 formatted date-time string
    citizenId: string; //Unique identifier for the citizen
}
