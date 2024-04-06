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

            <!-- Progress Bar -->
            <div style="background-color: lightgrey; width: 100%; height: 20px;">
                <div :style="{backgroundColor: totalCalories>suggestedCalories ? 'red' : 'green', width: progressPercentage + '%', height: '100%'}">
                </div>
            </div>

            <!-- Circular Progress Indicator -->
            <svg width="100" height="100" viewBox="0 0 100 100">
                <circle cx="50"
                        cy="50"
                        r="40"
                        fill="none"
                        stroke="lightgrey"
                        stroke-width="10" />
                <circle cx="50"
                        cy="50"
                        r="40"
                        fill="none"
                        :stroke="totalCalories > suggestedCalories ? 'red' : 'green'"
                        stroke-width="10"
                        stroke-linecap="round"
                        :stroke-dasharray="circumference"
                        :stroke-dashoffset="circumference * (1 - progressPercentage / 100)"
                        transform="rotate(-90,50,50)" />
                <text x="50" y="55" font-size="15" fill="black" text-anchor="middle">
                    {{ Math.min(progressPercentage, 100).toFixed(0) }}%
                </text>
            </svg>
            <!-- Protein Progress Bar -->
            <progress-bar :value="totalProtein" :max="suggestedProtein" color="blue"></progress-bar>
            <!-- Carbs Progress Bar -->
            <progress-bar :value="totalCarbs" :max="suggestedCarbs" color="orange"></progress-bar>
            <!-- Fat Progress Bar -->
            <progress-bar :value="totalFat" :max="suggestedFat" color="purple"></progress-bar>
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
            circumference() {
                const radius = 40; // Match the SVG circle's radius
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