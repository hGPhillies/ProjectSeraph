import type User from '../../domain/User.ts';
import MeasurementButton from './MeasurementButton.tsx';

function Home({ user }: User) {
    return (
        <div>
            {user.isNurse ?
                <p>nurse stuff</p>
                :
                <div>
                    <MeasurementButton text="Mål Blodtryk" link="/measurebloodpressure" />
                    <MeasurementButton text="Mål Blodsukker" link="/measurebloodsugar" />
                </div>}
        </div>
    );
}

export default Home;