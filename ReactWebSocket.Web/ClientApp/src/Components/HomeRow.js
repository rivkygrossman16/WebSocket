import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import axios from 'axios';
import { useAuthContext } from '../AuthContext';


const HomeRow = ({ Task }) => {
    const { job, status } = Task;
    return (
        <tr>
            <td>{job}</td>
            <td>{status}</td>
        </tr>
    );

}
export default HomeRow;