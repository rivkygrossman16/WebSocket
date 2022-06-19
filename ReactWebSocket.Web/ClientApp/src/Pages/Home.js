import React, { useEffect, useRef, useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import axios from 'axios';
import { useAuthContext } from '../AuthContext';
import HomeRow from '../Components/HomeRow';
import { HubConnectionBuilder } from '@microsoft/signalr';

const Home = () => {
    const [job, setTaskItem] = useState('');
    const [tasks, setTasks] = useState([]);
    const history = useHistory();
    const connectionRef = useRef(null);
    const { user } = useAuthContext();
    useEffect(() => {
        const connectToHub = async () => {
            const connection = new HubConnectionBuilder().withUrl("/chat").build();
            await connection.start();
            connectionRef.current = connection;

            connectionRef.current.invoke('getTasks');
            connection.on('getTasks', tasks => {
                setTasks(tasks);
            });
            connection.on('addTask', tasks => {
                setTasks(tasks);
                console.log(tasks);
            });
            connection.on('markAsDoing', tasks => {
                setTasks(tasks);
            });
            connection.on('markAsDone', tasks => {
                setTasks(tasks);
            });
        }


        connectToHub();


    }, []);

    const onDoingClick = async (task) => {

        connectionRef.current.invoke('markAsDoing', task);
    }
    const onDoneClick = async (task) => {
        connectionRef.current.invoke('markAsDone', task);
    }



    const onAddClick = async () => {
        connectionRef.current.invoke('addTask', { job});
        setTaskItem('');
    }
    return (
        <div>
            <div className="row">
                <div className="col-md-10">
                    <input type="text" name="task" className="form-control" placeholder="Task Title" value={job} onChange={e => setTaskItem(e.target.value)} />
                </div>
                <div className="col-md-2">
                    <button className="btn btn-primary btn-block" onClick={onAddClick} > Add Task</button></div>
            </div>
            <table className="table table-hover table-striped table-bordered mt-3">
                <thead>
                    <tr>
                        <th>Job</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    {tasks && tasks.map(p => <tr key={p.id}>
                        <td>{p.job}</td>
                        <td>{p.status == 0 && <button className='btn btn-info' onClick={() => onDoingClick(p)}>I'm doing this!</button>}
                            {((p.status == 1 || p.status == 2) && user.id != p.userId) && <button className='btn btn-warning' disabled='true'>{p.user.firstName} {p.user.lastName} is doing this</button>}
                            {((p.status == 1 || p.status == 2) && user.id == p.userId) && <button className='btn btn-success' onClick={() => onDoneClick(p)}>I am done!</button>}

                        </td>

                    </tr>)}
                </tbody
                ></table>
        </div>
    )
}
export default Home;