dotnet test /p:CollectCoverage=true /p:CoverletOutput=BuildReports\Coverage\ /p:CoverletOutputFormat=cobertura /p:Exclude=\"[*]System.*\"

dotnet reportgenerator "-reports:BuildReports\Coverage\coverage.cobertura.xml" "-targetdir:BuildReports\Coverage" -reporttypes:HTML;HTMLSummary 