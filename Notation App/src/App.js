import logo from './logo.svg';
import './App.css';
import { Component } from 'react';

class App extends Component {
  
  constructor(props) {
    super(props);
    this.state = {
      notes: []
    }
  }

  API_URL = "http://localhost:5056"; 

  // Add async keyword to the method
  async componentDidMount() {
    await this.refreshNotes(); // await to ensure that the component doesn't proceed to the next line of code until the API call is done
  }

  // Method to get data from the API into the notes array using fetch
  async refreshNotes() {
    fetch(this.API_URL + "/api/TodoApp/GetNotes")
      .then(response => response.json())
      .then(data => {
        this.setState({ notes: data });
      });
  }
  async addClick() {
    var newNotes = document.getElementById("newNotes").value;
    const data = new FormData();
    data.append("newNotes", newNotes);
    try {
        const response = await fetch(this.API_URL + "/api/TodoApp/AddNotes", {
            method: "POST",
            body: data
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const result = await response.json();
        alert(result);
        this.refreshNotes();
    } catch (error) {
        console.error('Error:', error);
    }
  }

  async deleteClick(id) {
    try {
      const response = await fetch(`${this.API_URL}/api/TodoApp/DeleteNotes?id=${id}`, {
        method: "DELETE"
      });
  
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
  
      const result = await response.json();
      console.log('Delete result:', result); // Log the result to console
      alert(result);
      this.refreshNotes();
    } catch (error) {
      console.error('Error:', error);
    }
    
  }
  
  render() {
    const { notes } = this.state;
    return (
      <div className="App">
        <div className="left-section">
            <h1> To Do App </h1>
            <input id="newNotes" /> &nbsp;
            <button onClick={() => this.addClick()}> Add Note </button>
            {notes.map((note, index) => (
              <p key={index}>
                <b> * {note.description} </b>
                <button onClick={() => this.deleteClick(note.id)}>Delete Note</button>
              </p>
            ))}
        </div>
      </div>);
  }
}
  

export default App;