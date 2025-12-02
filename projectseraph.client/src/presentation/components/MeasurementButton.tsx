import { Link } from 'react-router-dom';

//saettes saadan her, ikke i parametrene
interface MeasurementButtonProps {
    text: string;
    link: string;
}

function MeasurementButton({ text, link }) {
    return (
        <div>
            <Link to={link }>
                <button>
                    {text}
                </button>
            </Link>
        </div>
    )
}

export default MeasurementButton;