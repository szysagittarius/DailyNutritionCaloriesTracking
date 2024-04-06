<template>
    <div class="progress-container">
        <!--<div class="progress-bar"
         :style="{width: progressPercent + '%' ,backgroundColor: value> max ? 'red' : color, }"
        >
    </div>-->

        <div class="label">{{ label }}</div>
        <div class="progress-bar"
             :style="progressBarStyle">
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            label: String,
            value: Number,
            max: Number,
            color: String,
        },
        computed: {
            progressPercent() {
                return (this.value / this.max) * 100;
            },
            progressBarStyle() {
                const style = {
                    width: `${Math.min(this.value / this.max * 100, 100)}%`,
                    backgroundColor: this.value > this.max ? 'red' : this.color,
                    height: '100%',
                    borderRadius: '8px',
                    transition: 'width 0.3s ease',
                };
                console.log(style); // Debugging
                return style;
            },
        },
    };
</script>

<style>
    .progress-container {
        width: 100%;
        align-items: center;
        background-color: #eee;
        border-radius: 8px;
        height: 20px;
        margin: 10px 0;
    }
    .label {
        margin-right: 10px;
        /*Space between label and progress bar*/
    }

    .progress-bar {
        height: 100%;
        border-radius: 8px;
        transition: width 0.3s ease;
        position: relative;
    }
</style>
