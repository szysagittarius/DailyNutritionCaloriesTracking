<template>
    <div class="foodnutrition-component">
        <h1>Daily food nutrition</h1>
        <p>This component demonstrates fetching data from the server.</p>

        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
        </div>

        <div v-if="post" class="content">

            <table>
                <thead>
                    <tr>
                        <th>Food Item</th>
                        <th>Serve Amount</th>
                        <th>Total Calories</th>

                        <th>Total Carbs</th>
                        <th>Total Fat</th>
                        <th>Total Protein</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(entry, index) in entries" :key="index">
                        <td>
                            <input type="text"
                                   v-model="entry.name"
                                   @input="autocomplete(index)"
                                   :list="'food-list-' + index">
                            <datalist :id="'food-list-' + index">
                                <option v-for="food in filteredFoods(entry.name)" :value="food.name" :key="food.name"></option>
                            </datalist>
                        </td>
                        <td><input type="number" v-model.number="entry.amount" @input="calculateNutrients(index)"></td>

                        <td><input type="number" v-model.number="entry.carbs" @input="calculateNutrients(index)"></td>
                        <td><input type="number" v-model.number="entry.fat" @input="calculateNutrients(index)"></td>
                        <td><input type="number" v-model.number="entry.protein" @input="calculateNutrients(index)"></td>

                        <td>{{ entry.calories.toFixed(2) }}</td>
                        <td><button @click="removeEntry(index)">Remove</button></td>
                    </tr>
                </tbody>
            </table>
            <button @click="addEntry">Add Food Item</button>

            <!-- Display Total Calories and Suggested Limit -->
            <div>
                Total Calories: {{ totalCalories.toFixed(2) }} kcal / {{ suggestedCalories }} kcal
            </div>

            <!-- Circular Progress Indicator -->


            <svg width="220" height="220" viewBox="-10 -10 220 220">
                <circle cx="100"
                        cy="100"
                        r="80"
                        fill="none"
                        stroke="lightgrey"
                        stroke-width="20" />
                <circle cx="100"
                        cy="100"
                        r="80"
                        fill="none"
                        :stroke="totalCalories > suggestedCalories ? 'red' : 'green'"
                        stroke-width="20"
                        stroke-linecap="round"
                        :stroke-dasharray="circumference"
                        :stroke-dashoffset="circumference - (circumference * progressPercentage / 100)"
                        transform="rotate(-90 100 100)" />
                <text x="100" y="105"
                      font-size="30"
                      fill="black"
                      text-anchor="middle">
                    {{ Math.min(progressPercentage, 100).toFixed(0) }}%
                </text>
            </svg>




            <progress-bar label="Calories" :value="totalCalories" :max="suggestedCalories" color="green"></progress-bar>
            <progress-bar label="Protein" :value="totalProtein" :max="suggestedProtein" color="blue"></progress-bar>
            <progress-bar label="Carbs" :value="totalCarbs" :max="suggestedCarbs" color="orange"></progress-bar>
            <progress-bar label="Fat" :value="totalFat" :max="suggestedFat" color="purple"></progress-bar>


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

<script>
    import ProgressBar from './ProgressBar.vue';

    export default {
        components: {
            ProgressBar
        },
        data() {
            return {
                loading: false,
                post: null,
                //entries: [{ name: '', amount: 0, calories: 0 }],
                suggestedCalories: 2000, // Suggested daily total calories
                suggestedProtein: 50, // Example value, adjust as needed
                suggestedCarbs: 300, // Example value, adjust as needed
                suggestedFat: 70, // Example value, adjust as needed
                entries: [{ name: '', amount: 0, calories: 0, protein: 0, carbs: 0, fat: 0 }],
            };
        },
        props: {
            value: Number,
            max: Number,
            color: String,
        },
        computed: {
            totalCalories() {
                return this.entries.reduce((total, entry) => total + entry.calories, 0);
            },
            totalProtein() {
                return this.entries.reduce((total, entry) => total + entry.protein, 0);
            },
            totalCarbs() {
                return this.entries.reduce((total, entry) => total + entry.carbs, 0);
            },
            totalFat() {
                return this.entries.reduce((total, entry) => total + entry.fat, 0);
            },

            progressPercentage() {
                const percentage = (this.totalCalories / this.suggestedCalories) * 100;
                return Math.min(percentage, 100); // Cap the percentage at 100% to avoid overflow
            },
            progressPercentCarbs() {
                const percentage = (this.totalCarbs / this.suggestedCarbs) * 100;
                return percentage > 100 ? 100 : percentage; // Cap the percentage at 100
            },
            progressPercentFat() {
                const percentage = (this.totalFat / this.suggestedFat) * 100;
                return percentage > 100 ? 100 : percentage; // Cap the percentage at 100
            },
            progressPercentProtein() {
                const percentage = (this.totalProtein / this.suggestedProtein) * 100;
                return percentage > 100 ? 100 : percentage; // Cap the percentage at 100
            },
            circumference() {
                const radius = 80; // Match the SVG circle's radius
                return 2 * Math.PI * radius;
            }
        },
        methods: {
            fetchData() {
                this.loading = true;
                // Ensure the fetch URL is correct
                fetch('foodnutrition')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                    });
            },
            addEntry() {
                this.entries.push({ name: '', amount: 0, calories: 0 });
            },
            removeEntry(index) {
                this.entries.splice(index, 1);
            },
            calculateCalories(index) {
                const entry = this.entries[index];
                const foodItem = this.post.find(food => food.name === entry.name);
                if (foodItem) {
                    entry.calories = (foodItem.calories / 100) * entry.amount;
                }
            },
            calculateNutrients(index) {
                const entry = this.entries[index];
                // Assuming the 'post' data structure contains 'calories', 'protein', 'carbs', and 'fat' for each food item
                const foodItem = this.post.find(food => food.name === entry.name);
                if (foodItem) {
                    // Define the nutrients you want to calculate
                    const nutrients = ['calories', 'protein', 'carbs', 'fat'];

                    nutrients.forEach(nutrient => {
                        // For each nutrient, calculate the value based on the foodItem's value and the entry's amount
                        // Ensure your entry model has these properties initialized to avoid undefined errors
                        entry[nutrient] = (foodItem[nutrient] / 100) * entry.amount;
                    });
                }
            },
            autocomplete(index) {
                // Autocomplete logic if needed; here it triggers calculateCalories directly
                this.calculateCalories(index);
            },
            filteredFoods(searchTerm) {
                if (!searchTerm) return [];
                const lowerCaseTerm = searchTerm.toLowerCase();
                return this.post.filter(food => food.name.toLowerCase().includes(lowerCaseTerm));
            },
        },
        created() {
            this.fetchData();
        },
    };
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

.progress-container {
  width: 100%;
  background-color: #eee;
  border-radius: 8px;
  margin: 10px 0;
}
.progress-bar {
  height: 20px;
  border-radius: 8px;
  transition: width 0.3s ease;
}
</style>