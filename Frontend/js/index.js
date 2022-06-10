const baseUrl = "https://movierestapitobiasmikkel.azurewebsites.net/api/movies"
Vue.createApp({
    data(){
        return{
            moviesList: [],
            singleMovie: null,
            idToGetBy: null,
            deleteId: 0,
            addData: {"id":0,"name":"","lengthInMinutes":null,"country":""},
            updateData: {"id":null,"name":"","lengthInMinutes":null,"country":""},
            filterLength: null,
            jsDataList: [],
            jsfilterLength: 0
            

        }
    },
    async created(){
        await this.getAllMovies()
        this.filterJS()
    },
    methods: {

        async helperGetAndShow(url){
            try{
                const response = await axios.get(url)
                this.moviesList = await response.data
            } catch (ex){
                alert(ex.message)
            }
        },

        async getAllMovies(filter){
            let url = baseUrl
            if(filter != null){
                url = url + "?filter=" + filter
            }
            await this.helperGetAndShow(url)
        },

        async addMovie(){
            try{
                const response = await axios.post(baseUrl, this.addData)
                this.getAllMovies()
            } catch (ex){
                alert(ex.message)
            }
        },

        async deleteMovie(){
            const url = baseUrl + "/" + this.deleteId
            try{
                const response = await axios.delete(url)
                this.getAllMovies()
                this.filterJS()
            } catch (ex) {
                alert(ex.message)
            }
        },

        async getById(){
            const url = baseUrl + "/" + this.idToGetBy
            try{
                const response = await axios.get(url)
                this.singleMovie = response.data
            } catch (ex){
                alert(ex.message)
            }
        },

        async updateMovie(){
            const url = baseUrl + "/" + this.updateData.id
            try{
                const response = await axios.put(url, this.updateData)
                this.getAllMovies()
            } catch (ex) {
                alert(ex.message)
            }
        },

        async filterJS(){
            
            this.jsDataList = this.moviesList.slice()
            if(this.jsfilterLength != 0){
                this.jsDataList = this.jsDataList.filter(w => w.lengthInMinutes < this.jsfilterLength)

            }
            
        }, 

    }

}).mount('#app')