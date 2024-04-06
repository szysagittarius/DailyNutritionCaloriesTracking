<template>
    <div class="progress-container">
        <div class="progress-bar"
             :style="{width: progressPercent2 + '%' ,backgroundColor: value> max ? 'red' : color, }"
            >
        </div>
    </div>
</template>

<script>export default {
  props: {
    value: Number,
    max: Number,
    color: String,
  },
  computed: {
    progressPercent() {
      return (this.value / this.max) * 100;
    },
  },
};</script>

<style>
    .progress-container {
        width: 100%;
        background-color: #eee;
        border-radius: 8px;
        height: 20px;
        margin: 10px 0;
    }

    .progress-bar {
        height: 100%;
        border-radius: 8px;
        transition: width 0.3s ease;
    }
</style>
