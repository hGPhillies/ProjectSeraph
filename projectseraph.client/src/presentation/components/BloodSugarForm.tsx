import { Link } from 'react-router-dom';
//der er endnu ikke opsat form paa denne - button linker bare til home
//lige nu kan spinneren ikke gaa over 100 eller under 0, men man kan skrive mere ind - det skal paa sigt fikses!
function BloodSugarForm() {
    return (
        <div>
            <label>
                Indtast din blodsukkermåling her:
                <input
                    name="bloodSugar"
                    id="mmol/l"
                    type="number"
                    min="0"
                    max="100"
                />
            </label>
            <br />
            <Link to="/">
                <button>
                    Tryk her for at sende din blodsukkermåling til sygeplejersken
                </button>
            </Link>
        </div>
    )
}

export default BloodSugarForm;