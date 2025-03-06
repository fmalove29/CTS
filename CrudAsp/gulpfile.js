const gulp = require('gulp');
const sass = require('gulp-sass')(require('sass'));
const path = require('path');

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

const scssPath = path.join(__dirname, 'wwwroot/scss/custom.scss'); // Source SCSS file
const cssDest = path.join(__dirname, 'wwwroot/css'); // Output folder

// SCSS to CSS task
function styles() {
    return gulp.src(scssPath, { allowEmpty: true }) // Allow empty files to prevent errors
        .pipe(sass().on('error', sass.logError)) // Compile SCSS to CSS
        .pipe(gulp.dest(cssDest)); // Output to CSS folder
}

// Watch for changes
function watchFiles() {
    gulp.watch('wwwroot/scss/**/*.scss', styles);
}

// Default task
exports.default = gulp.series(styles, watchFiles);