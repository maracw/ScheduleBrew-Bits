class SchedulePage {

    constructor() {
        //state of the page
        //this needs to be the list -batch info in array
        this.state = {
            styleSearch:"",
            recipeSearch:"",
            endDate: null,
            startDate: null,
            batches: []
          };
      /*this.state = {
        customerId: "",
        customer: null,
        states: []
      };*/
      
      //testing on swagger first
      // instance variables that the app needs but are not part of the "state" of the application
      this.server = "https://localhost:7077/api"
      this.url = this.server + "/batch";
  
      // instance variables related to ui elements simplifies code in other places
      

      //this.$form = document.querySelector('#customerForm');
      //input for string search
      this.$recipeSearch = document.querySelector('#recipe-search');
      this.$recipeSearch = document.querySelector('#style-search');
      //buttons for date search
      this.$dateSearch30 = document.querySelector('#search-30');
      this.$dateSearch60 = document.querySelector('#search-60');
      this.$dateSearch6mo = document.querySelector('#search-6mo');

      //input for custom date
      this.$dateStart = document.querySelector('#start-date');
      this.$dateEnd = document.querySelector('#end-date');

      //button to go to add page
      this.$goToAdd = document.querySelector('#go-to-add');
  
      // call these methods to set up the page
  
      /* call these methods to set up the page*/
  
      this.bindAllMethods();
      this.FindAllBatches();  
    }
    //end of constructor

      //set up binding for all methods - very important step!
      //in other labs binding a method to an event handler happened in one line
      //with so many methods and buttons, it makes sense to have
      //the actions put into separate methods
  
    // any method that is used as part of an event handler must bind this to the class
    // not all of these methods need to be bound but it was easier to do them all as I wrote them
    bindAllMethods() {
      this.FindAllBatches = this.FindAllBatches.bind(this);
    
    }

    //calls get and returns all batches
    //call on page load
    FindAllBatches() {
        fetch(`${this.url}/`)
        .then(response => response.json())
        .then(data => { 
          if (data.status == 404) {
            alert('No batches found'); 
          }
          else {
            this.state.batches = data;
            console.log(this.state.batches);
          }
        })
        .catch(error => {
          alert('There was a problem getting the batches info!'); 
        });
      }

    
    //makes the html for one row
    buildTableRow(){

    }
   
  

  
    // calls fill row on each batch in the list
    fillTable() {

    }
  

  }
  
  // instantiate the js app when the html page has finished loading
  window.addEventListener("load", () => new SchedulePage());
  



function init(){
    document.getElementById("start-date").defaultValue=()=>{new Date()};

}
