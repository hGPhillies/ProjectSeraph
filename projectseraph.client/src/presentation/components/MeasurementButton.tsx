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