import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import BloodsugarService from "../../services/BloodsugarService";
import type { BloodsugarMeasurement } from "../../domain/BloodsugarMeasurement";

// Lige nu kan spinneren ikke gå over 100 eller under 0, men man kan skrive mere ind - det skal på sigt fikses!

function BloodsugarForm() {
    const [bloodSugar, setBloodSugar] = useState<string>("");
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState<boolean>(false);

    // På sigt skal laves en feedback/pop-up med "MÅLING SENDT + Værdierne" i stedet for navigate
    const navigate = useNavigate();

    // Validation - check if input field has value
    // Used to disable/enable submit-button
    const isFormValid = bloodSugar.trim() !== "";

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (!isFormValid) return;

        setLoading(true);
        setError(null);
        setSuccess(false);

        const measurement: BloodsugarMeasurement = {
            bloodSugar: Number(bloodSugar),
            timestamp: new Date().toISOString(),
            citizenId: "123", // REFACTOR: skal hentes fra logged-in user/session
        };

        try {
            await BloodsugarService.sendMeasurement(measurement);
            setSuccess(true);
            // Navigate to homepage after success
            navigate("/");
        } catch {
            setError("Noget gik galt ved afsendelse af blodsukkermåling.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <h2>Blodsukkermåling</h2>

            <form onSubmit={handleSubmit}>
                <div style={{ marginBottom: "10px" }}>
                    <label htmlFor="bloodSugar">
                        Indtast din blodsukkermåling her:
                    </label>

                    <input
                        name="bloodSugar"
                        id="bloodSugar"
                        type="number"
                        min={0}
                        max={100}
                        value={bloodSugar}
                        onChange={(e) => setBloodSugar(e.target.value)}
                        required
                        style={{ marginLeft: "8px" }}
                    />

                    <span style={{ marginLeft: "8px" }}>mmol/L</span>
                </div>

                {/* Mini-spinner */}
                <button type="submit" disabled={!isFormValid || loading}>
                    {loading
                        ? "Sender..."
                        : "Tryk her for at sende din blodsukkermåling til sygeplejersken"}
                </button>
            </form>

            {/* Feedback under form */}
            {error && <div style={{ color: "red", marginTop: "8px" }}>{error}</div>}
            {success && (
                <div style={{ color: "green", marginTop: "8px" }}>
                    Blodsukkermåling sendt!
                </div>
            )}

            {/* Go back to homepage */}
            <div style={{ marginTop: "24px" }}>
                <Link to="/">
                    <button type="button">Tilbage til forsiden</button>
                </Link>
            </div>
        </div>
    );
}

export default BloodsugarForm;
