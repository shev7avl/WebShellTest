import React, {useState, useEffect} from 'react'
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';

const ConsoleInput = (props) => {
    const [count, setCount] = useState(0);
    const [input, setInput] = useState('');
    const [latestInputs, setLatestInputs] = useState([]);

    const onInputSubmit = (e) => {
        e.preventDefault();

        const isInputProvided = input && input !== '';
        
        console.log('submitting input: ',input);

        if (isInputProvided) {
            
            props.sendRequest(input);

            setCount(count+1);
            
            latestInputs.push(input);
            setLatestInputs(latestInputs);
            console.log('input provided:', input)
            console.log('counter:', count)
            console.log('latest inputs:', latestInputs)
        }
        else {
            alert('Please enter command');
        }

    }

    const onInputUpdate = (e) => {
        console.log("Input updated:", e.target.value)
        setInput(e.target.value);
    }

    const onKeyDownListener = (e) =>
    {
      switch(e.key) {
        case "Up":
        case "ArrowUp": 
          console.log('Up arrow pressed');
          console.log('Current count: ', count);
          console.log('Inputs total: ', latestInputs.length)// up
          if(count > 0)
          {
            setCount(count-1);
            console.log('Input selected: ',latestInputs[count]);
            setInput(latestInputs[count]);
            
          }
          else 
          {
            console.log('Input selected: ',latestInputs[count]);
            setInput(latestInputs[count]);
          };
          break;

        case "Down":
        case "ArrowDown":
          console.log('Down arrow pressed');
          console.log('Current count: ', count);
          console.log('Inputs total: ', latestInputs.length);// down
          if(count < latestInputs.length-1)
          {
            setCount(count+1);
            console.log('Input selected: ',latestInputs[count]);
            setInput(latestInputs[count]);
          }
          else 
          {
            console.log('Input selected: ',latestInputs[count]);
            setInput(latestInputs[count]);

          };
          break;
    }
    }

    useEffect(() => {
      window.addEventListener('keydown', onKeyDownListener);
      return () => {
        window.removeEventListener('keydown', onKeyDownListener);
      };
      })
    

    return (
      <Form>
        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Control className="inputControl" type="command" placeholder="Enter cmd command" onChange={onInputUpdate} value={input}/>

        </Form.Group>
        <button variant="primary" type="submit" onClick={onInputSubmit} >
          Submit
        </button>
      </Form>
    );
  }
  
  export default ConsoleInput;