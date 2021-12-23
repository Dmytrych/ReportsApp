import {Button, Paper} from '@mui/material';
import Box from '@mui/material/Box';
import {useNavigate} from "react-router-dom";
import {useEffect, useState} from "react";

export default function GenerateReportPage({isAuthenticated}) {
    const navigate = useNavigate()
    const [response, setResponse] = useState({Text: ""})
    useEffect(() => {
        if(!isAuthenticated){
            navigate("/login")
        }
    }, [])

    let handleSubmit = async () => {
        let response = await fetch('http://localhost:8080/ReportsApi/Generate', {
            mode: 'cors',
            method: 'POST',
            headers: {
                'Content-Type': 'text/plain'
            },
            credentials: "include"
        }).catch(ex => console.log(ex))

        if (response && response.ok){
            setResponse(await response.json())
            return;
        }
        alert("Invalid Credentials")
    }

    return (
        <Box sx={{display: 'flex', justifyContent: 'center'}}>
            <Paper sx={{width: '70rem', display: 'flex', flexDirection: 'column', justifyContent: 'center'}}>
                <Box>
                    <Button onClick={handleSubmit}>Get Report</Button>
                </Box>
                <div dangerouslySetInnerHTML={{__html: response.Text.replaceAll("\n", "<br/>")}}></div>
            </Paper>
        </Box>
    )
}