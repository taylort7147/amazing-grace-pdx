// Barrier to wait for multiple results before executing a callback
class Barrier{
    constructor(expectedResults, callback){
        this.callback = callback;
        this.expectedResults = expectedResults;
        this.availableResults = {};
    }
    
    addResult(resultKey, resultValue){
        console.log(`addResult(${resultKey}, ...)`);
        this.availableResults[resultKey] = resultValue;
        if(this.expectedResults.every(key => key in this.availableResults)){
            if(callback){
                this.callback(this.availableResults);
                delete this.callback;
            }
        }
    }
}