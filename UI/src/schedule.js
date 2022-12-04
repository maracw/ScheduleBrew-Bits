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
        this.stylesFetched=false;
        this.batchesFetched=false;
      /*this.state = {
        customerId: "",
        customer: null,
        states: []
      };*/
      
      //testing on swagger first
      // instance variables that the app needs but are not part of the "state" of the application
      this.server = "https://localhost:5001/api"
      this.batchURL = this.server + "/Batch";
      this.recipeURL = this.server +"/Recipe";
      this.styleURL= this.server+"/Style";
  
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
      //nested array - fills all arrays
      this.FindAllRecipes(); 
    }
    //end of constructor

      //set up binding for all methods - very important step!
      //in other labs binding a method to an event handler happened in one line
      //with so many methods and buttons, it makes sense to have
      //the actions put into separate methods
  
    // any method that is used as part of an event handler must bind this to the class
    // not all of these methods need to be bound but it was easier to do them all as I wrote them
    bindAllMethods() {
      this.buildTableRow=this.buildTableRow.bind(this);
      this.fillTable=this.fillTable.bind(this);
      this.FindAllRecipes=this.FindAllRecipes.bind(this);
      //these work with the arrays in state
      this.getRecipeIndex=this.getRecipeIndex.bind(this);
      this.getStyleIndex=this.getStyleIndex.bind(this);

    }

    FindAllRecipes(){
        fetch(this.recipeURL)
        .then(response => response.json())
        .then(data => { 
            this.state.recipes = data;
            console.log("recipe data success");
            //now there are recipes in the array 
            })
        .then(a=>{
                for(let i=0; i<this.state.recipes.length; i++)
                {
                    let sId = this.state.recipes[i].styleId;
                    fetch(this.styleURL+'/id/'+sId)
                        .then(response => response.json())
                        .then(data => { 
                        this.state.styles.push(data); 
                        this.stylesFetched=true;
                        })
                    .catch(error => {
                    alert('There was a problem getting the indiv style info!'); 
                    })   
                }
                console.log("for loop succes");
                })//end of a
        .then(b=>{
                fetch(this.batchURL)
                    .then(response => response.json())
                    .then(data => { 
                             this.state.batches = data;
                             this.batchesFetched=true;
                            console.log("batches success");
                            })
                    .catch(error => {
                        alert('There was a problem getting the batches info!'); 
                        });
                    })
                    //end of b
        .then (c=>{
                this.fillTable();
                
                })
                //end of c
        .catch(error => {
          alert('There was a problem getting the recipe info!'); 
            });//end of catch for get all recipes
    }
    //helps with buildTableRow
    getStyleIndex(sId){
        let index=-1;
        for (let i=0; i<this.state.styles.length; i++)
        {   
            if(sId==this.state.styles[i].styleId)
            {
                index=i;
                return index;
            }
        }
        return index;
    }
    //helps with buildTableRow
    getRecipeIndex(id){
        let index=-1;
        for (let i=0; i<this.state.recipes.length; i++)
        {   
            if(id==this.state.recipes[i].recipeId)
            {
                index=i;
                return index;
            }
        }
        return index;
    }
    
    //makes the html for one row
    buildTableRow(batchObj){
        //all arrays should have data
        let rId=batchObj.recipeId;
        let sId="";
        let rIndex=-1;
        let sIndex=-1;
        let batchId=batchObj.batchId;
        let recipeName="not found";
        let styleName="not found";
        let recipeVersion="not found";
        let batchABV=batchObj.abv;
        let batchIBU=batchObj.ibu;
        let equipID=batchObj.equipmentId;
        let start=batchObj.scheduledStartDate;
        if(this.state.recipes.length!=0){
            rIndex=this.getRecipeIndex(rId);
            recipeName=this.state.recipes[rIndex].name;
            recipeVersion=this.state.recipes[rIndex].version;
            sId=this.state.recipes[rIndex].styleId;
        }
        if(this.state.styles.length!=0){
            sIndex=this.getStyleIndex(sId);
            styleName=this.state.styles[sIndex].name;
        }
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

        while(!this.stylesFetched||!this.batchesFetched){
            let i=0;
            i++;
        }
        for(let i=0; i<this.state.batches.length; i++)
        {
            tableHtml+=this.buildTableRow(this.state.batches[i]);
        }
        this.$tableBody.innerHTML=tableHtml;
        console.log("html success");
    }

  }
  
  let schedulePage
  // instantiate the js app when the html page has finished loading
  window.addEventListener("load", () => {schedulePage=new SchedulePage()});
  



function init(){
    document.getElementById("start-date").defaultValue=()=>{new Date()};

}
