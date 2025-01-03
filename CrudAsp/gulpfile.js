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
        
    gulp.src('node_modules/font-awesome/css/font-awesome.min.css')
        .pipe(gulp.dest('wwwroot/css/movie'));

    gulp.src('node_modules/font-awesome/css/font-awesome.css')
        .pipe(gulp.dest('wwwroot/css/movie'));
    
    gulp.src('node_modules/bootstrap/dist/js/bootstrap.min.js')
        .pipe(gulp.dest('wwwroot/js'));
    
    gulp.src('node_modules/jquery-easing/dist/jquery.easing.1.3.umd.min.js')
        .pipe(gulp.dest('wwwroot/js'));
        
    gulp.src('node_modules/material-icons/**/*')
        .pipe(gulp.dest('wwwroot/material-icons'));

    gulp.src('node_modules/select2/dist/js/select2.min.js')
        .pipe(gulp.dest('wwwroot/js'));

    gulp.src('node_modules/select2/dist/js/select2.js')
        .pipe(gulp.dest('wwwroot/js'));
    gulp.src('node_modules/select2/dist/css/select2.min.css')
        .pipe(gulp.dest('wwwroot/css'));

    gulp.src('node_modules/datatables.net/js/dataTables.min.js')
        .pipe(gulp.dest('wwwroot/js'));
    
    gulp.src('node_modules/datatables.net-dt/js/dataTables.dataTables.min.js')
        .pipe(gulp.dest('wwwroot/js'));

    gulp.src('node_modules/datatables/media/css/jquery.dataTables.min.css')
        .pipe(gulp.dest('wwwroot/css'));
        
    gulp.src('node_modules/datatables/media/js/jquery.dataTables.min.js')
        .pipe(gulp.dest('wwwroot/js'));
});

gulp.task('default', gulp.series('copy-dependencies'));

