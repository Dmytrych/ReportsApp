import {Button, Checkbox, Paper} from '@mui/material';
import Box from '@mui/material/Box';
import {useNavigate} from "react-router-dom";
import {useEffect, useState} from "react";
import TextField from "@mui/material/TextField";

export default function StudentManagePage({isAuthenticated}) {
    const navigate = useNavigate()
    const [student, setStudent] = useState({
        Name: "",
        Surname: "",
        FacultyName: "",
        BenefitCategory: ""
    })

    useEffect(() => {
        if(!isAuthenticated){
            navigate("/login")
        }
    }, [isAuthenticated])

    const handle = field => event => {
        setStudent(prevState =>({
            ...prevState,
            [field]: event.target.value
        }))
        console.log(student)
    };

    let handleSubmit = async () => {
        if (student){
            let response = await fetch('http://localhost:8080/StudentApi/Add', {
                mode: 'cors',
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain'
                },
                credentials: "include",
                body: JSON.stringify(student)
            }).catch(ex => console.log(ex))

            if (response && response.ok){
                navigate("/login")
                return;
            }
            alert("Invalid Credentials")
        }
    }

    return (
        <Box sx={{display: 'flex', justifyContent: 'center'}}>
            <Box sx={{width: '10rem', display: 'flex', flexDirection: 'column', justifyContent: 'center'}}>
                <Box>
                    <TextField onChange={handle('Name')} label="Name" margin="normal"/>
                </Box>
                <Box>
                    <TextField onChange={handle('Surname')} label="Surname" margin="normal"/>
                </Box>
                <Box>
                    <TextField onChange={handle('FacultyName')} label="Faculty Name" margin="normal"/>
                </Box>
                <Box>
                    <TextField onChange={handle('BenefitCategory')} label="Benefit Category" margin="normal"></TextField>
                </Box>
                <Box sx={{display: "flex", justifyContent:"center"}}>
                    <Button onClick={handleSubmit}>Add</Button>
                </Box>
            </Box>
        </Box>
    )
}