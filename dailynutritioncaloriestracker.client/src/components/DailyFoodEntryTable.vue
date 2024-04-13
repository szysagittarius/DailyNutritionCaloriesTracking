<template>
    <div>
        <table>
            <thead>
                <tr>
                    <th>Food Item</th>
                    <th>Serve Amount (g)</th>
                    <th>Total Carbs</th>
                    <th>Total Fat</th>
                    <th>Total Protein</th>
                    <th>Total Calories</th>
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
                    <td>{{ formatNumber(entry.carbs) }}</td>
                    <td>{{ formatNumber(entry.fat) }}</td>
                    <td>{{ formatNumber(entry.protein) }}</td>
                    <td>{{ entry.calories.toFixed(2) }}</td>
                    <td><button @click="removeEntry(index)">Remove</button></td>
                </tr>
            </tbody>
        </table>
        <button @click="addEntry">Add Food Item</button>
    </div>
</template>

<script>
    export default {
        props: {
            post: Array,
            entries: Array
        },
        methods: {
            addEntry() {
                this.entries.push({ name: '', amount: 0, calories: 0, carbs: 0, fat: 0, protein: 0 });
            },
            removeEntry(index) {
                this.entries.splice(index, 1);
            },
            calculateNutrients(index) {
                const entry = this.entries[index];
                const foodItem = this.post.find(food => food.name === entry.name);
                if (foodItem) {
                    entry.calories = (foodItem.calories / 100) * entry.amount;
                    entry.carbs = (foodItem.carbs / 100) * entry.amount;
                    entry.fat = (foodItem.fat / 100) * entry.amount;
                    entry.protein = (foodItem.protein / 100) * entry.amount;
                }
            },
            autocomplete(index) {
                this.calculateNutrients(index);
            },
            filteredFoods(searchTerm) {
                if (!searchTerm) return [];
                const lowerCaseTerm = searchTerm.toLowerCase();
                return this.post.filter(food => food.name.toLowerCase().includes(lowerCaseTerm));
            },
            formatNumber(value) {
                return Number(value).toFixed(2);
            },
        }
    }
</script>
