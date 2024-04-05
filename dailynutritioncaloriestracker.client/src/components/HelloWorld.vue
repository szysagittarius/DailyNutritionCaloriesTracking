<template>
    <div class="foodnutrition-component">
        <h1>food nutrition</h1>
        <p>This component demonstrates fetching data from the server.</p>

        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
        </div>

        <div v-if="post" class="content">
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Measure</th>
                        <th>Calories (kcal/100g)</th>
                        <th>Proteins (g/100g)</th>
                        <th>Carbohydrates (g/100g)</th>
                        <th>Fat (g/100g)</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="food in post" :key="food.name">
                        <td>{{ food.name }}</td>
                        <td>{{ food.measure }}</td>
                        <td>{{ food.calories }}</td>
                        <td>{{ food.protein }}</td>
                        <td>{{ food.carbs }}</td>
                        <td>{{ food.fat }}</td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                loading: false,
                post: null
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData() {
                this.post = null;
                this.loading = true;

                fetch('foodnutrition')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                        return;
                    });
            }
        },
    });
</script>

<style scoped>
th {
    font-weight: bold;
}
tr:nth-child(even) {
    background: #F2F2F2;
}

tr:nth-child(odd) {
    background: #FFF;
}

th, td {
    padding-left: .5rem;
    padding-right: .5rem;
}

.foodnutrition-component {
    text-align: center;
}

table {
    margin-left: auto;
    margin-right: auto;
}
</style>