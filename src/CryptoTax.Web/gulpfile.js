const gulp = require('gulp');

gulp.task('build', () => {
  const postcss = require('gulp-postcss');

  return gulp.src('./Assets/tailwind.css')
    .pipe(postcss([
      require('precss'),
      require('tailwindcss'),
    ]))
    .pipe(gulp.dest('./wwwroot/css/'));
});