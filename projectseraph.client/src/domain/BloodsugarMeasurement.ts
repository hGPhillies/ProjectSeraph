// TypeScript interface representing a bloodsugar measurement sent from the React client.
// This defines the exact shape of the JSON object submitted to the backend.
// The field is required and must be a number.
export interface BloodsugarMeasurement
{
    bloodSugar: number; //Bloodsugar level i mmol/L
    time: string; //ISO 8601 formatted date-time string 
    citizenId: string; //Unique identifier for the citizen
}