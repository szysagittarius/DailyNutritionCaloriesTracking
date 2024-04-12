<template>
    <ul class="timeline">
        <li v-for="(log, index) in logs" :key="log.date" class="timeline-entry">
            <div class="card">
                <h3>{{ log.date }}</h3>
                <p>Calories: {{ log.calories }}</p>
                <p>Carbs: {{ log.carbs }}</p>
                <p>Protein: {{ log.protein }}</p>
                <p>Fat: {{ log.fat }}</p>
            </div>
            <!-- Arrow element, not added for the last item -->
            <div v-if="index !== logs.length - 1" class="arrow-down"></div>
        </li>
    </ul>
</template>

<script>
export default {
  name: 'FoodLog',
  data() {
    return {
      logs: [
        { date: '2023-04-10', calories: 2000, carbs: '50g', protein: '100g', fat: '70g' },
        { date: '2023-04-11', calories: 1800, carbs: '45g', protein: '120g', fat: '60g' },
        // Add more logs here
      ]
    };
  }
};
</script>

<style>
    .timeline {
        list-style-type: none;
        padding: 0;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .timeline-entry {
        position: relative;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .card {
        background-color: #fff;
        border: 1px solid #ccc;
        border-radius: 8px;
        box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        padding: 20px;
        width: 300px; /* Adjust width as necessary */
        transition: transform 0.3s ease-in-out;
    }

        .card:hover {
            transform: translateY(-5px);
            box-shadow: 0 6px 16px rgba(0,0,0,0.15);
        }

    .arrow-down {
        width: 2px; /* This makes it look like a line */
        height: 30px; /* Adjust height for spacing between cards */
        background-color: #ccc; /* Line color */
        position: relative;
        bottom: -10px;
    }

        .arrow-down::after {
            content: '';
            width: 0;
            height: 0;
            border-left: 10px solid transparent;
            border-right: 10px solid transparent;
            border-top: 10px solid #ccc; /* Arrow color */
            position: absolute;
            top: 100%;
            left: -9px; /* Centers the arrow */
        }

    h3 {
        color: #333;
        font-size: 18px;
        margin-bottom: 10px;
    }

    p {
        color: #666;
        font-size: 16px;
        margin: 5px 0;
    }
</style>
