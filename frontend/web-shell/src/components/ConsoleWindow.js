import { Stack } from "react-bootstrap";
import Response from "./Response";
import './ConsoleWindow.css'
 
const ConsoleWindow = (props) => {

    const shell = props.shell.map(m => <Response 
        key={Date.now() * Math.random()}
        input={m.input}
        output={m.output}
        error={m.error}/>
)

    return (
        <Stack gap={3}>
            <p className="Title">Шелл для шиндовс</p>

            <div className="Window" 
            style={{
                height: '343px',
                width: '677px',
                overflow: 'auto',
                backgroundColor: 'black',
                color: 'white',
                msScrollbarBaseColor: 'gold',
                fontFamily : 'consolas',
                padding: '10px'
            }} >
                {shell}
            </div>              
        <p></p>
        </Stack>
        
    );
  }
  
  export default ConsoleWindow;