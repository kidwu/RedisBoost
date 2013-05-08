﻿namespace NBoosters.RedisBoost.Misk
{
	internal class BuffersPool: IBuffersPool
	{
		public int BufferSize { get; set; }
		private readonly ObjectsPool<byte[]> _pool = new ObjectsPool<byte[]>();
		private int _poolSize;

		public int MaxPoolSize { get; set; }

		public BuffersPool(int defaultBufferSize, int defaultMaxPoolSize)
		{
			BufferSize = defaultBufferSize;
			MaxPoolSize = defaultMaxPoolSize;
		}

		public bool TryGet(out byte[] buffer)
		{
			buffer = _pool.GetOrCreate(() => 
				(++_poolSize > MaxPoolSize) ? null : new byte[BufferSize]);
			return buffer != null;
		}

		public void Release(byte[] buffer)
		{
			_pool.Release(buffer);
		}
	}
}
