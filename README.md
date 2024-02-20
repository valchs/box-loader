## Box loader
Home assignment for VR Group
### Setup:
- Clone repository
- Update connection string in `BoxLoaderDbContext`
```
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Your connection string");
	}
```
- Set `BoxLoader.Host` as default project and run command `dotnet ef database update -p ./BoxLoader.Repository` to add tables to DB
- Application uses the first command line argument as a path to monitor so set it in visual studio project properties or when launching project with `dotnet run`
- Run the project, drop a file in selected directory and observe changes

### Notes
- I added some notes (search by `NOTE:`) in code where I skipped some parts to save time.
