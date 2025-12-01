import { Link } from 'react-router-dom';
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import BloodpressureService from "../../services/BloodpressureService";
import type { BloodpressureMeasurement } from "../../domain/BloodpressureMeasurement";

function BloodpressureForm() {
    const [systolic, setSystolic] = useState<string>(""); 
    const [diastolic, setDiastolic] = useState<string>(""); 
    const [pulse, setPulse] = useState<string>(""); 
    const [loading, setLoading] = useState<boolean>(false); 
    const [error, setError] = useState<string | null>(null); 
    const [success, setSuccess] = useState<boolean>(false); 


    //REFACTOR - her navigerer vi til forsiden efter måling er sendt
    //På sigt skal laves en feedback/pop-up med "MÅLING SENDT + Værdierne"
    const navigate = useNavigate(); 

    //Validation - check if all inputfields has value
    //Used to disable/enable submit-button
    const isFormValid =
        systolic.trim() !== "" &&
        diastolic.trim() !== "" &&
        pulse.trim() !== ""; 

    const handleSubmit = async (
        event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (!isFormValid) return; 

        setLoading(true);
        setError(null);
        setSuccess(false);

        const measurement: BloodpressureMeasurement =
        {
            systolic: Number(systolic),
            diastolic: Number(diastolic),
            pulse: Number(pulse),
        };

        try {
            await BloodpressureService.sendMeasurement(measurement);
            setSuccess(true);
            //Navigate to homepage after success
            navigate("/");
        } catch {
            setError("Noget gik galt ved afsendelse af blodtryksmåling.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <h2>Blodtryksmåling</h2>

            <form onSubmit={handleSubmit}>
            <table>
                <tbody>
                    <tr>
                        <td>
                            <label htmlFor="systolic">Systolisk:</label>
                        </td>
                        <td>
                                <input
                                    name="systolic"
                                    id="systolic"
                                    type="number"
                                    min={0}
                                    max={300}
                                    value={systolic}
                                    onChange={(e) => setSystolic(e.target.value)}
                                    required
                            />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label htmlFor="diastolic">Diastolisk:</label>
                        </td>
                        <td>
                                <input
                                    name="diastolic"
                                    id="diastolic"
                                    type="number"
                                    min={0}
                                    max={300}
                                    value={diastolic}
                                    onChange={(e) => setDiastolic(e.target.value)}
                                    required
                            />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label htmlFor="pulse">Puls:</label>
                        </td>
                        <td>
                                <input
                                    name="pulse"
                                    id="pulse"
                                    type="number"
                                    min={0}
                                    max={300}
                                    value={pulse}
                                    onChange={(e) => setPulse(e.target.value)}
                                    required
                            />
                        </td>
                    </tr>
                </tbody>
                </table>

                <br />
                    <button type="submit" disabled={!isFormValid || loading}>
                        {loading ? "Sender..." : "Tryk her for at sende din blodtryksmåling til sygeplejersken"}   
                    </button>
            </form>

            {/*Feedback under form*/}
            {error && <div style={{ color: "red" }}>{error}</div>}
            {success && <div style={{ color: "green" }}>Blodtryksmåling sendt!</div>}

            <br />

            {/*Go back to homepage*/}
            <Link to="/"><button>Tilbage til forsiden</button></Link>
        </div>
    );
}

export default BloodpressureForm;