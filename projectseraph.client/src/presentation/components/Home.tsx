import MeasurementButton from './MeasurementButton.tsx';

function Home() {
    return (
        <div>
            <MeasurementButton text="Mål Blodtryk" link="/measurebloodpressure" />
            <MeasurementButton text="Mål Blodsukker" link="/measurebloodsugar" />
        </div>
    );
}

export default Home;