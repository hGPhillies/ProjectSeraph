import { Link } from 'react-router-dom';
function BloodpressureForm() {
    return (
        <div>
            <table>
                <tbody>
                    <tr>
                        <td>
                            <label>Systolisk:</label>
                        </td>
                        <td>
                            <input
                                name="systolic"
                                id="systolic"
                                type="number"
                                min="0"
                                max="300"
                            />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Diastolisk:</label>
                        </td>
                        <td>
                            <input
                                name="diastolic"
                                id="diastolic"
                                type="number"
                                min="0"
                                max="300"
                            />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Pulse:</label>
                        </td>
                        <td>
                            <input
                                name="pulse"
                                id="pulse"
                                type="number"
                                min="0"
                                max="300"
                            />
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <Link to="/">
                <button>
                    Tryk her for at sende din blodtryksmåling til sygeplejersken
                </button>
            </Link>
        </div>
    )
}

export default BloodpressureForm;