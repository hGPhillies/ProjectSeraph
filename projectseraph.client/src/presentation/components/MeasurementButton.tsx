interface MeasurementButtonProps {
    text: string;
    route: string;
}

function MeasurementButton({ text, route }) {
    return (
        <div>
            <button>
                { text }
            </button>
        </div>
    )
}

export default MeasurementButton;