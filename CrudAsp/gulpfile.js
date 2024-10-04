var gulp = require('gulp');

gulp.task('copy-dependencies', function() {
    // Copy Bootstrap CSS to wwwroot/css
    gulp.src('node_modules/jquery/dist/jquery.min.js')
        .pipe(gulp.dest('wwwroot/js'));

    // Copy jQuery to wwwroot/js
    gulp.src('node_modules/axios/dist/axios.min.js')
        .pipe(gulp.dest('wwwroot/js'));

    gulp.src('node_modules/sweetalert2/dist/sweetalert2.min.js')
        .pipe(gulp.dest('wwwroot/js'));    
        
});

gulp.task('default', gulp.series('copy-dependencies'));

