<?php

date_default_timezone_set('Europe/Moscow'); // Set this to your local timezone - http://www.php.net/manual/en/timezones.php


/**
 * Автодеплой из битбакета
 *
 * Class Deploy
 */
class Deploy
{
	/**
	 * Адрес файла лога
	 *
	 * @var string
	 */
	private $_logfile = '/var/log/deploy.log';
	
	/**
	 * Папка где лежит репозиторий
	 *
	 * @var
	 */
	private $_repo_path;
	
	/**
	 * Ветка, куда надо спулить
	 *
	 * @var string
	 */
	private $_branch;
	
	/**
	 * Имя удаленного репозитория
	 *
	 * @var string
	 */
	private $_remote;
	
	
	/**
	 * Deploy constructor.
	 *
	 * @param $repo_dir
	 * @param string $branch
	 * @param string $remote
	 */
	public function __construct($repo_dir, $branch = 'master', $remote = 'origin')
	{
		$this->_repo_path = realpath($repo_dir) . DIRECTORY_SEPARATOR;
		
		$this->_branch = $branch;
		$this->_remote = $remote;
		
		$this->log('Attempting deployment...');
	}
	
	
	/**
	 * Логирование происходящего
	 *
	 * @param $somecontent
	 * @param string $type
	 */
	private function log($somecontent, $type = 'INFO')
	{
		$handle      = fopen($this->_logfile, 'a+');
		$somecontent = date('Y-m-d H:i:s ') . ' [' . $type . ']: ' . $somecontent . "\n";
		fwrite($handle, $somecontent);
		fclose($handle);
	}
	
	
	/**
	 * Установка адрес лог файла
	 *
	 * @param string $path
	 */
	public function set_log_file($path = '/var/log/deploy.log')
	{
		$this->_logfile = $path;
	}
	
	/**
	 * Выполнение необходимого для pull запроса
	 */
	public function execute()
	{
		try {
			// Make sure we're in the right directory
			chdir($this->_repo_path);
			$this->log('Changing working directory... ');
			// Discard any changes to tracked files since our last deploy
			exec('git reset --hard HEAD', $output);
			$this->log('Reseting repository... ' . implode(' ', $output));
			// Update the local repository
			exec('git pull ' . $this->_remote . ' ' . $this->_branch, $output);
			$this->log('Pulling in changes... ' . implode(' ', $output));
			
			$this->log('Deployment successful.' . "\n");
		} catch (Exception $e) {
			$this->log($e, 'ERROR');
		}
	}
}