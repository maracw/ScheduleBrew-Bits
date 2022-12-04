class SchedulePage {

    constructor() {
        //state of the page
        //this needs to be the list -batch info in array
        this.state = {
            styleSearch:"",
            recipeSearch:"",
            endDate: null,
            startDate: null,
            batches: [],
            recipes: [],
            styles: []
          };
      /*this.state = {
        customerId: "",
        customer: null,
        states: []
      };*/
      
      //testing on swagger first
      // instance variables that the app needs but are not part of the "state" of the application
      this.server = "https://localhost:7077/api"
      this.batchURL = this.server + "/Batch";
      this.recipeURL = this.server +"/Recipe";
  
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

      //table
      this.$tableBody=document.querySelector('tbody');
      //button to go to add page
      this.$goToAdd = document.querySelector('#go-to-add');
  
      // call these methods to set up the page
  
      /* call these methods to set up the page*/
  
      this.bindAllMethods();
      this.FindAllRecipes();
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
      this.buildTableRow=this.buildTableRow.bind(this);
      this.fillTable=this.fillTable.bind(this);
      this.FindAllRecipes=this.FindAllRecipes.bind(this);
      this.getRecipeName=this.getRecipeName.bind(this);
    
    }
    //try just getting all the recipes first
    FindAllRecipes(){
        fetch(this.recipeURL)
        .then(response => response.json())
        .then(data => { 
            this.state.recipes = data;
            console.log(this.state.recipes);
            //this.fillTable();
        })
        .catch(error => {
          alert('There was a problem getting the batches info!'); 
        });
    }
    //calls get and returns all batches
    //call on page load
    FindAllBatches() {
        fetch(this.batchURL)
        .then(response => response.json())
        .then(data => { 
            this.state.batches = data;
            console.log(this.state.batches);
            const html=this.buildTableRow(this.state.batches[0]);
            this.$tableBody.innerHTML=html;
            //this.fillTable();
        })
        .catch(error => {
          alert('There was a problem getting the batches info!'); 
        });
      }

    getRecipeName(id){
        let name=""
        for (let i=0; i<this.state.recipes.length; i++)
        {   
            if(id==this.state.recipes[i].recipeId)
            {
                name=this.state.recipes[i].name;
            }
        }
        return name;
    }
    //makes the html for one row
    buildTableRow(batchObj){
        let batchId=batchObj.batchId;
        let recipeName= this.getRecipeName(batchObj.recipeId);
        let styleName=""
        let recipeVersion="";
        let batchABV=batchObj.abv;
        let batchIBU=batchObj.ibu;
        let equipID=batchObj.equipmentId;
        let start=batchObj.scheduledStartDate;

        let htmlRow=`
        <tr id="${batchId}">
        <td name="recipe">${recipeName}</td>
        <td name="style">${styleName}</td>
        <td name="version">${recipeVersion}</td>
        <td name="abv">${batchABV}</td>
        <td name="ibu">${batchIBU}</td>
        <td name="history"><button href="#" class="rounded-pill custom-btn">View</button></td>
        <td name="ingredients"><button href="#" class="rounded-pill custom-btn">View</button></td>
        <td name="inventory"><button href="#" class="rounded-pill custom-btn">View</button></td>
        <td name="equip">${equipID}</td>
        <td name="schedStart">${start}</td>
        <td></td>
      </tr>`
      return htmlRow;
    }
   
  

  
    // calls fill row on each batch in the list
    fillTable() {
        let tableHtml=""
        for(let i=0; i<this.state.batches.length; i++)
        {
            tableHtml+=this.buildTableRow(this.state.batches[i]);
        }
        this.$tableBody.innerHTML=tableHtml;
    }
  

  }
  
  let schedulePage
  // instantiate the js app when the html page has finished loading
  window.addEventListener("load", () => {schedulePage=new SchedulePage()});
  



function init(){
    document.getElementById("start-date").defaultValue=()=>{new Date()};

}
